"use strict";

app.controller('FormEmailCtrl', ["$uibModalInstance", "spiderdocsService", "comService", "$uibModal", "tagService", "title", "subject", "body", "to", "cc", "bcc", "attachments", "en_file_Status", "en_Actions", function ($uibModalInstance, spiderdocsService, comService, $uibModal, tagService, title, subject, body, to, cc, bcc, attachments, en_file_Status, en_Actions) {
  var $ctrl, checkForm, init, validateTogo;
  $ctrl = this;
  $ctrl.errors = [];
  $ctrl.title = title;
  $ctrl.email = {
    subject: subject,
    to: to,
    cc: cc,
    bcc: bcc,
    attachments: attachments != null ? attachments : [],
    body: ''
  };
  $ctrl.show = {
    cc: false,
    bcc: false
  };

  init = function init() {
    validateTogo();
  };

  validateTogo = function validateTogo() {
    $ctrl.canGo = false;

    if (checkForm()) {
      $ctrl.canGo = true;
    }

    return $ctrl.canGo;
  };

  checkForm = function checkForm() {
    var email, hasError, i, len, ref;
    $ctrl.errors = [];
    hasError = true;

    if (!!$ctrl.email.subject && !!$ctrl.email.to && !!$ctrl.email.body) {
      hasError = false;
    }

    if ($ctrl.email) {
      ref = $ctrl.email.to.split(';'); //when email isnt ''

      for (i = 0, len = ref.length; i < len; i++) {
        email = ref[i];
        hasError = !comService.validateEmail(email);

        if (hasError === true) {
          $ctrl.errors.push("Email format must be valid.");
        }
      }
    }

    return !hasError;
  };

  $ctrl.en_file_Status = en_file_Status;
  $ctrl.canGo = false;
  $ctrl.selectedAttrs = {};
  $ctrl.defaultAttrs = {};
  init();

  $ctrl.folderclicked = function () {
    openFolderTree([]);
  };

  $ctrl.ok = function () {
    var _bcc, _cc, _to, form, img, ref, ref1, ref2;

    _to = (ref = function () {
      var i, len, ref1, results;
      ref1 = $ctrl.email.to.split(';');
      results = [];

      for (i = 0, len = ref1.length; i < len; i++) {
        to = ref1[i];

        if (!!to) {
          results.push(to);
        }
      }

      return results;
    }()) != null ? ref : [];
    _cc = (ref1 = function () {
      var i, len, ref2, results;
      ref2 = $ctrl.email.cc.split(';');
      results = [];

      for (i = 0, len = ref2.length; i < len; i++) {
        cc = ref2[i];

        if (!!cc) {
          results.push(cc);
        }
      }

      return results;
    }()) != null ? ref1 : [];
    _bcc = (ref2 = function () {
      var i, len, ref3, results;
      ref3 = $ctrl.email.bcc.split(';');
      results = [];

      for (i = 0, len = ref3.length; i < len; i++) {
        bcc = ref3[i];

        if (!!bcc) {
          results.push(bcc);
        }
      }

      return results;
    }()) != null ? ref2 : [];
    form = {
      to: _to,
      cc: _cc,
      bcc: _bcc,
      subject: $ctrl.email.subject,
      body: $ctrl.email.body,
      attachments: function () {
        var i, len, ref3, results;
        ref3 = $ctrl.email.attachments;
        results = [];

        for (i = 0, len = ref3.length; i < len; i++) {
          img = ref3[i];
          results.push(img.filename);
        }

        return results;
      }()
    };
    $uibModalInstance.close(form);
  };

  $ctrl.cancel = function () {
    $uibModalInstance.dismiss('cancel');
  };

  $ctrl.validateTogo = validateTogo;
}]);
//# sourceMappingURL=../../../maps/app/controllers/forms/form-email-controller.js.map
