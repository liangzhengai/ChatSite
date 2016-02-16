
$(document).ready(function () {

    new chat({
        me: {
            id: $("#uid").val(),
            alias: $("#alias").val(),
            avatar: baseUrl + "/assets/images/avatar_me.png"
        },
        socket: socket
    });

});