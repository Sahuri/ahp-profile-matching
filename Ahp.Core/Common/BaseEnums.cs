using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahp.Core.Common
{
    public static class BaseEnums
    {

        public static string SetStatus(string status)
        {
            string result = "";
            switch ((EnumStatus)Enum.Parse(typeof(EnumStatus), status))
            {
                //0
                case EnumStatus.Draft_Request_Budget:
                    result = EnumStatus.Draft_Request_Budget.ToString().ReplaceUnderScoreToSpace();
                    break;

                //-1
                case EnumStatus.Submit_Request_Budget:
                    result = EnumStatus.Submit_Request_Budget.ToString().ReplaceUnderScoreToSpace();
                    break;
                //1
                case EnumStatus.Waiting_for_approval_by_Receiver_Budget_Holder_Mgr:
                    result = EnumStatus.Waiting_for_approval_by_Receiver_Budget_Holder_Mgr.ToString().ReplaceUnderScoreToSpace();
                    break;
                //2
                case EnumStatus.Waiting_for_approval_by_Receiver_Budget_Holder_VP:
                    result = EnumStatus.Waiting_for_approval_by_Receiver_Budget_Holder_VP.ToString().ReplaceUnderScoreToSpace();
                    break;
                //3
                case EnumStatus.Waiting_for_approval_by_Receiver_Budget_Holder_DIR:
                    result = EnumStatus.Waiting_for_approval_by_Receiver_Budget_Holder_DIR.ToString().ReplaceUnderScoreToSpace();
                    break;
                //4
                case EnumStatus.Approved_by_Receiver_Budget_Holder_Mgr:
                    result = EnumStatus.Approved_by_Receiver_Budget_Holder_Mgr.ToString().ReplaceUnderScoreToSpace();
                    break;
                //5
                case EnumStatus.Rejected_by_Receiver_Budget_Holder_Mgr:
                    result = EnumStatus.Rejected_by_Receiver_Budget_Holder_Mgr.ToString().ReplaceUnderScoreToSpace();
                    break;

                //6
                case EnumStatus.Approved_by_Receiver_Budget_Holder_VP:
                    result = EnumStatus.Approved_by_Receiver_Budget_Holder_VP.ToString().ReplaceUnderScoreToSpace();
                    break;
                //7
                case EnumStatus.Rejected_by_Receiver_Budget_Holder_VP:
                    result = EnumStatus.Rejected_by_Receiver_Budget_Holder_VP.ToString().ReplaceUnderScoreToSpace();
                    break;
                //8
                case EnumStatus.Approved_by_Receiver_Budget_Holder_DIR:
                    result = EnumStatus.Approved_by_Receiver_Budget_Holder_DIR.ToString().ReplaceUnderScoreToSpace();
                    break;
                //9
                case EnumStatus.Rejected_by_Receiver_Budget_Holder_DIR:
                    result = EnumStatus.Rejected_by_Receiver_Budget_Holder_DIR.ToString().ReplaceUnderScoreToSpace();
                    break;
                //10
                case EnumStatus.Waiting_for_approval_by_Sender_Budget_Holder_Mgr:
                    result = EnumStatus.Waiting_for_approval_by_Sender_Budget_Holder_Mgr.ToString().ReplaceUnderScoreToSpace();
                    break;
                //11
                case EnumStatus.Waiting_for_approval_by_Sender_Budget_Holder_VP:
                    result = EnumStatus.Waiting_for_approval_by_Sender_Budget_Holder_VP.ToString().ReplaceUnderScoreToSpace();
                    break;
                //12
                case EnumStatus.Waiting_for_approval_by_Sender_Budget_Holder_DIR:
                    result = EnumStatus.Waiting_for_approval_by_Sender_Budget_Holder_DIR.ToString().ReplaceUnderScoreToSpace();
                    break;
                //13
                case EnumStatus.Approved_by_Sender_Budget_Holder_Mgr:
                    result = EnumStatus.Approved_by_Sender_Budget_Holder_Mgr.ToString().ReplaceUnderScoreToSpace();
                    break;
                //14
                case EnumStatus.Rejected_by_Sender_Budget_Holder_Mgr:
                    result = EnumStatus.Rejected_by_Sender_Budget_Holder_Mgr.ToString().ReplaceUnderScoreToSpace();
                    break;
                //15
                case EnumStatus.Approved_by_Sender_Budget_Holder_VP:
                    result = EnumStatus.Approved_by_Sender_Budget_Holder_VP.ToString().ReplaceUnderScoreToSpace();
                    break;
                //16
                case EnumStatus.Rejected_by_Sender_Budget_Holder_VP:
                    result = EnumStatus.Rejected_by_Sender_Budget_Holder_VP.ToString().ReplaceUnderScoreToSpace();
                    break;
                //17
                case EnumStatus.Approved_by_Sender_Budget_Holder_DIR:
                    result = EnumStatus.Approved_by_Sender_Budget_Holder_DIR.ToString().ReplaceUnderScoreToSpace();
                    break;
                //18
                case EnumStatus.Rejected_by_Sender_Budget_Holder_DIR:
                    result = EnumStatus.Rejected_by_Sender_Budget_Holder_DIR.ToString().ReplaceUnderScoreToSpace();
                    break;
                //19
                case EnumStatus.Waiting_for_approval_by_Verificator:
                    result = EnumStatus.Waiting_for_approval_by_Verificator.ToString().ReplaceUnderScoreToSpace();
                    break;
                //20
                case EnumStatus.Approved_by_Verificator:
                    result = EnumStatus.Approved_by_Verificator.ToString().ReplaceUnderScoreToSpace();
                    break;
                //21
                case EnumStatus.Rejected_by_Verificator:
                    result = EnumStatus.Rejected_by_Verificator.ToString().ReplaceUnderScoreToSpace();
                    break;
                //22
                case EnumStatus.Waiting_for_approval_by_Submiter_Mgr_Fin:
                    result = EnumStatus.Waiting_for_approval_by_Submiter_Mgr_Fin.ToString().ReplaceUnderScoreToSpace();
                    break;
                //23
                case EnumStatus.Waiting_for_approval_by_Submiter_Mgr_PE:
                    result = EnumStatus.Waiting_for_approval_by_Submiter_Mgr_PE.ToString().ReplaceUnderScoreToSpace();
                    break;
                //24
                case EnumStatus.Waiting_for_approval_by_Submiter_Mgr_MA_PDA:
                    result = EnumStatus.Waiting_for_approval_by_Submiter_Mgr_MA_PDA.ToString().ReplaceUnderScoreToSpace();
                    break;
                //25
                case EnumStatus.Approved_by_Submiter_Mgr_Fin:
                    result = EnumStatus.Approved_by_Submiter_Mgr_Fin.ToString().ReplaceUnderScoreToSpace();
                    break;
                //26
                case EnumStatus.Rejected_by_Submiter_Mgr_Fin:
                    result = EnumStatus.Rejected_by_Submiter_Mgr_Fin.ToString().ReplaceUnderScoreToSpace();
                    break;
                //27
                case EnumStatus.Approved_by_Submiter_Mgr_PE:
                    result = EnumStatus.Approved_by_Submiter_Mgr_PE.ToString().ReplaceUnderScoreToSpace();
                    break;

                //28
                case EnumStatus.Rejected_by_Submiter_Mgr_PE:
                    result = EnumStatus.Rejected_by_Submiter_Mgr_PE.ToString().ReplaceUnderScoreToSpace();
                    break;
                //29
                case EnumStatus.Approved_by_Submiter_Mgr_MA_PDA:
                    result = EnumStatus.Approved_by_Submiter_Mgr_MA_PDA.ToString().ReplaceUnderScoreToSpace();
                    break;
                //30
                case EnumStatus.Rejected_by_Submiter_Mgr_MA_PDA:
                    result = EnumStatus.Rejected_by_Submiter_Mgr_MA_PDA.ToString().ReplaceUnderScoreToSpace();
                    break;
                //31
                case EnumStatus.Waiting_for_approval_Mgr_Fin:
                    result = EnumStatus.Waiting_for_approval_Mgr_Fin.ToString().ReplaceUnderScoreToSpace();
                    break;
                //32
                case EnumStatus.Waiting_for_approval_Mgr_PE:
                    result = EnumStatus.Waiting_for_approval_Mgr_PE.ToString().ReplaceUnderScoreToSpace();
                    break;
                //33
                case EnumStatus.Waiting_for_approval_GM:
                    result = EnumStatus.Waiting_for_approval_GM.ToString().ReplaceUnderScoreToSpace();
                    break;
                //34
                case EnumStatus.Waiting_for_approval_VP_Controller:
                    result = EnumStatus.Waiting_for_approval_VP_Controller.ToString().ReplaceUnderScoreToSpace();
                    break;
                //35
                case EnumStatus.Approved_Mgr_Fin:
                    result = EnumStatus.Approved_Mgr_Fin.ToString().ReplaceUnderScoreToSpace();
                    break;
                //36
                case EnumStatus.Approved_Mgr_PE:
                    result = EnumStatus.Approved_Mgr_PE.ToString().ReplaceUnderScoreToSpace();
                    break;
                //37
                case EnumStatus.Approved_GM:
                    result = EnumStatus.Approved_GM.ToString().ReplaceUnderScoreToSpace();
                    break;
                //38
                case EnumStatus.Approved_VP_Controller:
                    result = EnumStatus.Approved_VP_Controller.ToString().ReplaceUnderScoreToSpace();
                    break;
                //39
                case EnumStatus.Rejected_Mgr_Fin:
                    result = EnumStatus.Rejected_Mgr_Fin.ToString().ReplaceUnderScoreToSpace();
                    break;
                //40
                case EnumStatus.Rejected_Mgr_PE:
                    result = EnumStatus.Rejected_Mgr_PE.ToString().ReplaceUnderScoreToSpace();
                    break;
                //41
                case EnumStatus.Rejected_GM:
                    result = EnumStatus.Rejected_GM.ToString().ReplaceUnderScoreToSpace();
                    break;
                //42
                case EnumStatus.Rejected_VP_Controller:
                    result = EnumStatus.Rejected_VP_Controller.ToString().ReplaceUnderScoreToSpace();
                    break;
                //43
                case EnumStatus.Complete_Approver:
                    result = EnumStatus.Complete_Approver.ToString().ReplaceUnderScoreToSpace();
                    break;
                //44
                case EnumStatus.Finish:
                    result = EnumStatus.Finish.ToString().ReplaceUnderScoreToSpace();
                    break;
                //45
                case EnumStatus.Reject:
                    result = EnumStatus.Reject.ToString().ReplaceUnderScoreToSpace();
                    break;
                //46
                case EnumStatus.Approved_by_Budget_Holder_Mgr:
                    result = EnumStatus.Approved_by_Budget_Holder_Mgr.ToString().ReplaceUnderScoreToSpace();
                    break;
            }

            return result;
        }

        public enum EnumStatus
        {
            Draft_Request_Budget = 0,
            Submit_Request_Budget=-1,
            Waiting_for_approval_by_Receiver_Budget_Holder_Mgr = 1,
            Waiting_for_approval_by_Receiver_Budget_Holder_VP = 2,
            Waiting_for_approval_by_Receiver_Budget_Holder_DIR = 3,
            Approved_by_Receiver_Budget_Holder_Mgr = 4,
            Rejected_by_Receiver_Budget_Holder_Mgr = 5,
            Approved_by_Receiver_Budget_Holder_VP = 6,
            Rejected_by_Receiver_Budget_Holder_VP = 7,
            Approved_by_Receiver_Budget_Holder_DIR = 8,
            Rejected_by_Receiver_Budget_Holder_DIR = 9,

            Waiting_for_approval_by_Sender_Budget_Holder_Mgr = 10,
            Waiting_for_approval_by_Sender_Budget_Holder_VP = 11,
            Waiting_for_approval_by_Sender_Budget_Holder_DIR = 12,
            Approved_by_Sender_Budget_Holder_Mgr = 13,
            Rejected_by_Sender_Budget_Holder_Mgr = 14,
            Approved_by_Sender_Budget_Holder_VP = 15,
            Rejected_by_Sender_Budget_Holder_VP = 16,
            Approved_by_Sender_Budget_Holder_DIR = 17,
            Rejected_by_Sender_Budget_Holder_DIR = 18,

            Waiting_for_approval_by_Verificator = 19,
            Approved_by_Verificator = 20,
            Rejected_by_Verificator = 21,

            Waiting_for_approval_by_Submiter_Mgr_Fin = 22,
            Waiting_for_approval_by_Submiter_Mgr_PE = 23,
            Waiting_for_approval_by_Submiter_Mgr_MA_PDA = 24,
           
            Approved_by_Submiter_Mgr_Fin = 25,
            Rejected_by_Submiter_Mgr_Fin = 26,
            Approved_by_Submiter_Mgr_PE = 27,
            Rejected_by_Submiter_Mgr_PE = 28,
            
            Approved_by_Submiter_Mgr_MA_PDA = 29,
            Rejected_by_Submiter_Mgr_MA_PDA = 30,

            Waiting_for_approval_Mgr_Fin = 31,
            Waiting_for_approval_Mgr_PE = 32,
            Waiting_for_approval_GM = 33,
            Waiting_for_approval_VP_Controller = 34,
            Approved_Mgr_Fin = 35,
            Approved_Mgr_PE = 36,
            Approved_GM = 37,
            Approved_VP_Controller = 38,
            Rejected_Mgr_Fin = 39,
            Rejected_Mgr_PE = 40,
            Rejected_GM = 41,
            Rejected_VP_Controller = 42,

            Complete_Approver = 43,
            Finish = 44,
            Reject = 45,

            Approved_by_Budget_Holder_Mgr = 46
        }
    }
}
