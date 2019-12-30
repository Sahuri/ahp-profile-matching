/**
 * Created by griga on 2/9/16.
 */

// http://l-lin.github.io/angular-datatables/archives/#!/gettingStarted
angular.module('app.tables').controller('GenericDatatablesCtrl', function (DTOptionsBuilder, DTColumnBuilder, BASE_API) {
    this.standardOptions = DTOptionsBuilder
        .newOptions()
        .withOption('ajax', {
            // Either you specify the AjaxDataProp here
            // dataSrc: 'data',
            url: BASE_API + 'masterstatuslahan/datatables',
            type: 'POST'
        })
        .withDataProp('data')
        .withOption('processing', false)
        .withOption('serverSide', true)
        //.withPaginationType('full_numbers')
         //Add Bootstrap compatibility
        .withDOM("<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
            "t" +
            "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>")
        .withBootstrap();
    
    this.standardColumns = [
        DTColumnBuilder.newColumn('Kode').withTitle('Kode').withClass('text-primary'),
        DTColumnBuilder.newColumn('Nama').withTitle('Nama')
    ];
});