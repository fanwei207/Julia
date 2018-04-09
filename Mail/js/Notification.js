function NotificationCenter($scope) {
    var permissionLevels = {};
    permissionLevels[notify.PERMISSION_GRANTED] = 0;
    permissionLevels[notify.PERMISSION_DEFAULT] = 1;
    permissionLevels[notify.PERMISSION_DENIED] = 2;

    $scope.isSupported = notify.isSupported;
    $scope.permissionLevel = permissionLevels[notify.permissionLevel()];

    $scope.getClassName = function () {
        if ($scope.permissionLevel === 0) {
            return "allowed"
        } else if ($scope.permissionLevel === 1) {
            return "default"
        } else {
            return "denied"
        }
    }

    $scope.callback = function () {
        console.log("test");
    }

    $scope.requestPermissions = function () {
        notify.requestPermission(function () {
            $scope.$apply($scope.permissionLevel = permissionLevels[notify.permissionLevel()]);
        })
    }
}
function show(bodyText) {
    notify.createNotification("提示:", { body: bodyText, icon: "./Notify/alert.ico" });
}