using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using Ahp.Core.Repositories.Abstractions;
using Ahp.Core.Repositories.Abstractions.Administrasi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Ahp.Core.Repositories.Concretes.Administrasi
{
    public class AdministrasiMenuRepository : GenericDataRepository<AdministrasiMenu, IGenericContext>, IAdministrasiMenuRepository
    {
        protected IGenericContext ctx;

        private List<string> KodeAksesMenus = new List<string>();
        public AdministrasiMenuRepository(IGenericContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }

        public override bool Create(AdministrasiMenu model)
        {
            model.Kode = Guid.NewGuid().ToString().Substring(0, 15);

            model.Parent = string.IsNullOrEmpty(model.Parent) ? "#" : model.Parent;

            return base.Create(model);
        }

        public override List<Dropdown> Dropdown(AdministrasiMenu model, string term)
        {
            {
                var records = new List<RecursiveMenu>();
                if (string.IsNullOrEmpty(term))
                {
                    records = this.p_GetRecursiveMenu();//.Where(x => x.Url != "#").OrderBy(x => x.Index).ToList(); 
                }
                else
                {
                    records = this.p_GetRecursiveMenu().Where(x => x.Url != "#")
                    .Where(x => x.Title.ToLower()
                        .Contains(term.ToLower())).OrderBy(x => x.Index).ToList();
                }

                var dropdown = records.Select(x => new Dropdown()
                {
                    id = x.Kode,
                    value = x.Kode,
                    text = x.TitleWithPath
                }).ToList();

                return dropdown;
            }
        }

        public override List<Dropdown> DropdownByKey(AdministrasiMenu model, string term)
        {
            var records = new List<RecursiveMenu>();
            if (string.IsNullOrEmpty(term))
            {
                records = this.p_GetRecursiveMenu();
            }
            else
            {
                records = this.p_GetRecursiveMenu()
                .Where(x => x.Kode.ToLower()
                    .Contains(term.ToLower())).ToList();
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.Kode,
                value = x.Kode,
                text = x.TitleWithPath
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }

        #region IAdministrasiMenuRepository Members


        public List<MenuItem> GetMenuByUser()
        {
            //var user = base.User;
            var menuItem = new List<MenuItem>();
            var isAdministrator = false;
            using (IAdministrasiUserRepository repoUser = new AdministrasiUserRepository(ctx))
            {
               // repoUser.User = user;
                isAdministrator = repoUser.IsAdministrator;
            }

            if (isAdministrator)
            {
                this.p_GetRecursiveMenu().Where(x => x.Parent == "#").ToList().ForEach(x =>
                {
                    var mnuItem = 
                new MenuItem()
                {
                    kode = x.Kode,
                    parent = x.Parent,
                    title = x.Title,
                    icon = x.Icon,
                    href = null,
                    sref = x.Url,
                    Index = x.Index
                };

                    menuItem.Add(mnuItem);
                });


                this.KodeAksesMenus = this.p_GetRecursiveMenu().Select(x => x.Kode).ToList();
                p_GenerateMenu(menuItem);
            }
            else
            {
                var reckRoleUser = ctx.Set<AdministrasiRoleUser>().Where(x => x.KodeUser == "").Select(x => x.KodeRole).ToList();
                var recAksesRole = ctx.Set<AdministrasiHakAksesRole>().Where(x => reckRoleUser.Contains(x.KodeRole)).Select(x => x.KodeHakAkses).ToList();
                var recAksesMenu = ctx.Set<AdministrasiHakAksesMenu>().Where(x => recAksesRole.Contains(x.KodeHakAkses)).Select(x => x.KodeMenu).ToList();
                var recMenu = p_GetRecursiveMenu().Where(x => recAksesMenu.Contains(x.Kode)).ToList();
                var recParentMenu = recMenu.Select(x => x.Parent).Distinct().ToList();

                this.KodeAksesMenus = recAksesMenu;

                menuItem = p_GetParentMenu(recParentMenu);
                recMenu.Where(x => x.Parent == "#").ToList().ForEach(x =>
                {
                    var itm = new MenuItem()
                    {
                        kode = x.Kode,
                        parent = x.Parent,
                        title = x.Title,
                        icon = x.Icon,
                        href = null,
                        sref = x.Url,
                        Index = x.Index
                    };

                    menuItem.Add(itm);
                });


                p_GenerateMenu(menuItem.Distinct().ToList());
            }
            
            return menuItem.OrderBy(x => x.Index).ToList();
        }

        private List<MenuItem> p_GetParentMenu(List<string> lastParent)
        {
            var ParentMenus = new List<MenuItem>();
            var recParent = p_GetRecursiveMenu().Where(x => lastParent.Contains(x.Kode));
            var recParentDistinct = recParent.Select(x => x.Parent).Distinct().ToList();
            
            foreach (var rec in recParent)
            {
                var kode = rec.Parent == "#" ? rec.Kode : rec.Parent;

                var menu = p_GetRecursiveMenu().FirstOrDefault(x => x.Kode == kode);
                if (menu != null)
                {
                    var itm = new MenuItem()
                    {
                        kode = menu.Kode,
                        parent = menu.Parent,
                        title = menu.Title,
                        icon = menu.Icon,
                        href = menu.Parent == "#" ? menu.Url : null,
                        sref = menu.Parent == "#" ? null : menu.Url,
                        Index = menu.Index
                    };

                    var check = ParentMenus.Where(x => x.kode == itm.kode).FirstOrDefault();
                    if (check == null)
                    {
                        ParentMenus.Add(itm);
                    }
                    p_GetParentMenu(recParentDistinct);
                }
            }

            return ParentMenus;
        }

        private void p_GenerateMenu(List<MenuItem> parent)
        {
            foreach (var rec in parent)
            {
                var parents = new List<MenuItem>();
                var menu = p_GetRecursiveMenu().Where(x => x.Parent == rec.kode).ToList();
                menu.ForEach(x =>
                {
                    if (p_IsParent(x.Kode))
                    {
                        var i = 1;
                        var recs = p_GetRecursiveMenu().Where(x2 => x2.Parent == x.Kode).ToList();
                        recs.ForEach(x2 =>
                        {
                            if (p_IsParent(x2.Kode))
                            {
                                if (i == 1)
                                {
                                    var child = new MenuItem()
                                    {
                                        kode = x.Kode,
                                        parent = x.Parent,
                                        title = x.Title,
                                        icon = x.Icon,
                                        href = x.Url,
                                        sref = null,
                                        Index = x.Index
                                    };

                                    parents.Add(child);
                                }
                            }
                            {
                                if (i == 1)
                                {
                                    if (this.KodeAksesMenus.Contains(x2.Kode))
                                    {
                                        var child = new MenuItem()
                                        {
                                            kode = x.Kode,
                                            parent = x.Parent,
                                            title = x.Title,
                                            icon = x.Icon,
                                            href = null,
                                            sref = x.Url,
                                            Index = x.Index
                                        };

                                        parents.Add(child);
                                    }
                                }
                            }
                            i++;
                        });
                    }
                    else
                    {
                        if (this.KodeAksesMenus.Contains(x.Kode))
                        {
                            var child = new MenuItem()
                            {
                                kode = x.Kode,
                                parent = x.Parent,
                                title = x.Title,
                                icon = x.Icon,
                                href = null,
                                sref = x.Url,
                                Index = x.Index
                            };

                            parents.Add(child);
                        }
                    }
                });

                rec.items = parents;
                if (rec.items.Count() > 0)
                    p_GenerateMenu(rec.items);
            }
        }

        private bool p_IsParent(string kode)
        {
            var recs = p_GetRecursiveMenu().Where(x => x.Parent == kode).ToList();

            return recs.Count() > 0;
        }

        private bool p_IsExistChild(string kode)
        {
            var found = false;
            var recs = p_GetRecursiveMenu().Where(x => x.Parent == kode).ToList();
            recs.ForEach(x =>
            {
                var recChild = p_GetRecursiveMenu().Where(y => y.Parent == x.Kode).ToList();
                recChild = recChild.Where(y => this.KodeAksesMenus.Contains(y.Kode)).ToList();
                if (recChild.Count > 0)
                {
                    found = true;
                }
            });

            return found;
        }

        private List<RecursiveMenu> p_GetRecursiveMenu()
        {
            var p0 = new SqlParameter("@KodeUser", SqlDbType.VarChar);
            p0.Value = this.UserProfile.UserID;

            SqlParameter[] param = { p0 };

            return ctx.Database.SqlQuery<RecursiveMenu>("exec sp_GetRecursiveMenu @KodeUser", param).ToList();
        }
        #endregion
    }
}
