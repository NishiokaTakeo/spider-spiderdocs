"use strict";

app.controller('formReviewCtrl', ['$scope', '$uibModal', '$uibModalInstance', 'comService', 'spiderdocsService', 'en_file_Status', 'doc', 'currentReview', 'en_ReviewStaus', 'permissions', 'en_Actions', 'reviewUsers', '$q', 'isOwner', 'en_file_Sp_Status', 'reviewHistory', 'profile', 'users', 'en_ReviewAction', function ($scope, $uibModal, $uibModalInstance, comService, spiderdocsService, en_file_Status, doc, currentReview, en_ReviewStaus, permissions, en_Actions, reviewUsers, $q, isOwner, en_file_Sp_Status, reviewHistory, profile, users, en_ReviewAction) {
  var $ctrl, init, populateForms, ref;
  $ctrl = this;
  $scope.model = {
    comment: '',
    reviewers: reviewUsers,
    owner_comment: currentReview.owner_comment,
    owner_review: (ref = users.find(function (u) {
      return u.id === currentReview.owner_id;
    })) != null ? ref.name : void 0,
    review_history: reviewHistory
  };
  $scope.doc = {};
  $scope.title = "Start New Review";
  $scope.canGo = false;
  $scope.validation = {};
  angular.extend($scope.doc, doc);
  $scope.tab = 1;

  init = function init() {
    if ($scope.hasNotStarted()) {
      $scope.model.action = 'start';
    }

    if (!$scope.hasNotStarted()) {
      if ($scope.hasNotReviewed()) {
        $scope.model.action = 'finish';
      }
    }

    populateForms();
    $scope.validateTogo();
  };

  $scope.checkoutInReview = function () {
    if ($scope.checkouted()) {
      alert('This has already checkedout');
      return;
    }

    comService.loading(true);
    spiderdocsService.CheckOut([doc.id]).then(function (err) {
      if (false === angular.isString(err)) {
        doc.id_status = en_file_Status.checked_out;
        Notify('The document has been checkedout!', null, null, 'success');
        $interval(function () {
          $window.location.href = './workspace/local';
        }, 1200, 1);
      } else {
        Notify('Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger');
      }
    }).then(function () {
      comService.loading(false);
    }).catch(function () {
      comService.loading(false);
    });
  };

  $scope.download = function () {
    return spiderdocsService.GetDonloadUrls(doc.id_latest_version).then(function (urls) {
      var url;

      if (urls.length > 0) {
        url = urls.pop();
        Notify("A download has been started. Check 'Download' Folder.", null, null, 'success');
        return comService.downloadBy(url);
      } else {
        return Notify("Rejected. You might not have right permission to do that (" + doc.name_folder + ").", null, null, 'danger');
      }
    }).then(function () {
      return comService.loading(false);
    }).catch(function () {
      return comService.loading(false);
    });
  };

  $scope.ok = function () {
    var pass, ref1, reviewer, reviewers;
    reviewers = (ref1 = function () {
      var i, len, ref2, results;
      ref2 = $scope.model.reviewers;
      results = [];

      for (i = 0, len = ref2.length; i < len; i++) {
        reviewer = ref2[i];

        if (reviewer.checked === true) {
          results.push(reviewer);
        }
      }

      return results;
    }()) != null ? ref1 : [];
    pass = {
      checked: $scope.model.action,
      review: {
        id_version: doc.id_latest_version,
        allow_checkout: $scope.model.allow_checkout,
        id_users: reviewers.map(function (u) {
          return u.id;
        }),
        deadline: moment($scope.model.deadline).format('YYYY-MM-DD HH:mm:ss'),
        owner_comment: $scope.model.comment
      }
    };

    if ($scope.model.allow_checkout === true) {
      $q.all(function () {
        var i, len, results;
        results = [];

        for (i = 0, len = reviewers.length; i < len; i++) {
          reviewer = reviewers[i];

          if (reviewer.checked === true) {
            results.push(spiderdocsService.GetFolderPermissionsAsync(doc.id_folder, reviewer.id));
          }
        }

        return results;
      }()).then(function (response) {
        var result;

        if (function () {
          var i, len, results;
          results = [];

          for (i = 0, len = response.length; i < len; i++) {
            result = response[i];

            if (result[en_Actions.CheckIn_Out].value === 1) {
              results.push(result);
            }
          }

          return results;
        }().length === response.length) {
          return $uibModalInstance.close(pass);
        } else {
          return Notify('All reviewers must have checkout permission.', null, null, 'danger');
        }
      });
    } else {
      $uibModalInstance.close(pass);
    }
  };

  $scope.cancel = function () {
    $uibModalInstance.dismiss('cancel');
  };

  $scope.checklength = function () {
    return $scope.model.comment.length >= 10;
  };

  $scope.hasSelectedReviewers = function () {
    var checked, i, len, ref1, reviewer;
    ref1 = $scope.model.reviewers;

    for (i = 0, len = ref1.length; i < len; i++) {
      reviewer = ref1[i];

      if (reviewer.checked === true) {
        checked = reviewer.checked;
      }
    }

    return checked != null ? checked : false;
  };

  $scope.checkCheckoutPermission = function () {
    var checked;
    checked = $scope.model.allow_checkout === true && $scope.hasPermission2Checout() || $scope.model.allow_checkout === false;
    return checked != null ? checked : false;
  };

  $scope.validateTogo = function () {
    $scope.canGo = false;

    if ($scope.hasNotStarted()) {
      if ($scope.checklength() && $scope.hasSelectedReviewers()) {
        //and $scope.checkCheckoutPermission()
        $scope.canGo = true;
      }
    } else if ($scope.hasNotReviewed()) {
      if ($scope.checklength()) {
        $scope.canGo = true;
      }
    }

    return $scope.canGo;
  };

  $scope.checkouted = function () {
    if (typeof doc.id_status !== 'number') {
      $log.error('doc.id_status must be number');
      debugger;
    }

    return doc.id_status === en_file_Status.checked_out;
  }; // $scope.isOwner = () ->
  // $scope.isnotstartedreviewUnreviewed = () ->
  // 	currentReview.status is en_ReviewStaus.UnReviewed
  // $scope.hasPermission2Review = () ->
  // 	permissions[en_Actions.Review] is 1


  $scope.hasPermission2Checout = function () {
    var ans;
    ans = false;

    if (!$scope.checkouted() && !$scope.isArchived() && permissions[en_Actions.CheckIn_Out].value === 1) {
      if ($scope.hasNotStarted()) {
        //or $scope.hasNotReviewed() and $scope.isReviewer()
        ans = true;
      }
    }

    return ans;
  };

  $scope.isArchived = function () {
    return doc.id_status === en_file_Status.archived;
  };

  $scope.hasNotReviewed = function () {
    return currentReview.status === en_ReviewStaus.UnReviewed;
  };

  $scope.hasNotStarted = function () {
    return currentReview.status === en_ReviewStaus.NotInReview;
  };

  $scope.hasReviewed = function () {
    return currentReview.status === en_ReviewStaus.Reviewed;
  };

  $scope.isViewedByOwner = function () {
    return isOwner;
  };

  $scope.isReviewer = function () {
    var id, ref1, ref2;
    id = (ref1 = (ref2 = currentReview.review_users.find(function (u) {
      return u.id_user === profile.id;
    })) != null ? ref2.id_user : void 0) != null ? ref1 : 0;
    return id > 0;
  };

  $scope.finishMyReview = function () {
    var ans, reviewer;
    ans = false;
    reviewer = currentReview.review_users.find(function (u) {
      return u.id_user === profile.id;
    });

    if (reviewer != null && reviewer.action === en_ReviewAction.Finalize) {
      ans = true;
    }

    return ans;
  }; // not review owner and not reviewer


  $scope.isViewedByUser = function () {
    return !isOwner && !$scope.isReviewer();
  };

  $scope.isOverdueReview = function () {
    return doc.id_sp_status === en_file_Sp_Status.review_overdue;
  };

  $scope.comment = function (comment) {
    return $scope.currentComment = comment;
  };

  populateForms = function populateForms() {
    if ($scope.hasNotStarted()) {
      return $scope.model.deadline = new Date(); // $scope.model.deadline_date = new Date()
      // $scope.model.deadline_time = new Date()
      // $scope.model.allow_checkout = false
      // $scope.model.allow_checkout = false
      // $scope.model.owner_comment
    } else if ($scope.hasNotReviewed()) {
      return $scope.model.deadline = moment(currentReview.deadline).toDate();
    } else if ($scope.isViewedByOwner()) {
      return $scope.model.deadline = moment(currentReview.deadline).toDate();
    } else if ($scope.isViewedByUser()) {
      return $scope.model.deadline = moment(currentReview.deadline).toDate();
    } else if ($scope.isReviewer()) {
      return $scope.model.deadline = moment(currentReview.deadline).toDate();
    }
  };

  init();
}]);
//# sourceMappingURL=../../../maps/app/controllers/forms/form-review-controller.js.map
