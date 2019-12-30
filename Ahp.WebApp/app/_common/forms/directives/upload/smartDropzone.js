'use strict';

angular.module('SmartAdmin.Forms').directive('smartDropzone', function () {
    return function (scope, element, attrs) {
        var btnRemoveAll = '<div class="form-group">\
                <button id="btnRemoveAllAttachment" type="button" class="btn btn-primary">\
                    <i class="fa fa-eraser"></i> Bersihkan Riwayat Berkas\
                </button>\
            </div>'
        element.before(btnRemoveAll);

        var config, dropzone;

        config = scope[attrs.smartDropzone];

        // create a Dropzone for the element with the given options
        var opt = {
            dictRemoveFile: 'Hapus riwayat berkas',
            dictCancelUpload: 'Membatalkan berkas',
            dictCancelUploadConfirmation: 'Yakin akan membatalkan unggahan berkas?',
            addRemoveLinks: true,
            maxFiles: 20,
            parallelUploads: 20,
            uploadMultiple: true
        }
        angular.extend(config.options, opt);
        config.options.init = function () {
            var myDropZone = this;
            $("#btnRemoveAllAttachment").click(function () {
                myDropZone.removeAllFiles(true);
            }
            );
        };

        dropzone = new Dropzone(element[0], config.options);
        // bind the given event handlers
        angular.forEach(config.eventHandlers, function (handler, event) {
            dropzone.on(event, handler);
        });
    };
});
