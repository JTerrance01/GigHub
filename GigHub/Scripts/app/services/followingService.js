var FollowingService = function () {

    var createFollowing = function (followeeId, done, fail) {
        $.post("/api/followings", { followeeId: button.attr("data-user-id") })
        .done(done)
        .fail(fail);
    }

    var deleteFolling = function (followeeId, done, fail) {
        $.ajax({
            url: "/api/followings/" + followeeId,
            type: "DELETE"
        })
        .done(done)
        .fail(fail);
    };

    return {
        createFollowing: createFollowing,
        deleteFolling: deleteFolling
    }
}();