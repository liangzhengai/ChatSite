

function CAddressWindow() {
    
}

CAddressWindow.prototype.init = function () {
    
    $([
        '<div class="chat">',
            '<div class="title">Friends</div>',
            '<div class="users">',
            '</div>',
        '</div>'
    ].join('')).appendTo('body');

    $(".chat > .title").on("click", function () {
        $(".users").toggleClass("hide");
    });
}

CAddressWindow.prototype.setUserList = function (me, userlist) {

    var pthis = this;

    console.log(userlist);

    if (userlist == undefined || userlist == null) {
        console.log("No user");
    } else {
        $(".users").empty();

        for (p in userlist) {
            if (userlist[p].id != me.id)
                $(".users").append(this.renderUser(userlist[p]));
        }

        $(".users > div.one-user").on("click", function () {
            $(pthis).trigger("openchat", $(this).data("id"));
        });
    }
}

CAddressWindow.prototype.addUser = function (user) {
    var pthis = this;
    $(this.renderUser(user)).appendTo(".users").on("click", function () {
        $(pthis).trigger("openchat", $(this).data("id"));
    });
}

CAddressWindow.prototype.removeUser = function (user) {
    $(".users div[name=user-" + user.id + "]").remove();
}

CAddressWindow.prototype.renderUser = function (user) {
    
    return [
        '<div name="user-',user.id,'" data-id="',user.id,'" class="clearfix one-user">',
            '<div style="clear:both;float:left;"><img style="width:32px;height:32px;" src="', user.avatar, '"></div>',
            '<div style="float:left;padding-left:1em;">', user.alias, '</div>',
        '</div>'
    ].join('');
}

function CChatWindow( user ) {
    this.user = user;
}

CChatWindow.prototype.openWindow = function (rpos) {

    var pthis = this;

    pthis.wnd =
        $([
        '<div class="chat-wnd">',
            '<div class="clearfix chat-title">',
                '<div style="clear:both;float:left">', this.user.alias, '</div>',
                '<div class="close" style="float:right"><i class="remove icon"></i></div>',
            '</div>',
            '<div class="chat-body">',
                '<div class="chat-msgs">',
                '</div>',
                '<div class="chat-input">',
                    '<textarea></textarea>',
                '</div>',
            '</div>',
        '</div>'
    ].join('')).appendTo('body').css('right', rpos);

    $(pthis.wnd).find(".chat-title").on("click", function () {
        $(".chat-body").toggleClass("hide");
    });

    $(pthis.wnd).find(".close").on("click", function () {
        $(pthis.wnd).remove();
        $(pthis).trigger("close", pthis.user.id);
    });

    $(pthis.wnd).find("textarea").on("keydown", function (evt) {
        if (evt.ctrlKey && (evt.which == 13 || evt.keyCode == 13)) {
            console.log("onEnter");
            console.log(pthis.user);
            $(pthis).trigger("sendmessage", { receiver: pthis.user, message: $(this).val().replace(/(?:\r\n|\r|\n)/g, "<br>") });
            $(this).val("");
        }
    });
}

CChatWindow.prototype.closeWindow = function () {
    $(this.wnd).remove();
}

CChatWindow.prototype.maximizeWindow = function () {
    $(this.wnd).find(".chat-body").removeClass("hide");
}

CChatWindow.prototype.addMessage = function (msg) {

    var sender = msg.sender;
    var receiver = msg.receiver;
    var message = msg.message;

    var cls = "";
    if (this.user.id != sender.id) {

        $([
            '<div class="clearfix">',
                '<div style="display:inline-block;"><img style="width:32px;height:32px;" src="', sender.avatar, '"></div>',
                '<div style="display:inline-block;width:220px;">',
                '<div class="bubble">', message, '</div>',
                '</div>',
            '</div>'
        ].join('')).appendTo($(this.wnd).find(".chat-msgs"));
    } else {

        $([
            '<div class="clearfix">',
                '<div style="display:inline-block;width:220px;">',
                '<div class="bubble bubble--alt">', message, '</div>',
                '</div>',
                '<div style="display:inline-block;"><img style="width:32px;height:32px;" src="', sender.avatar, '"></div>',
            '</div>'
        ].join('')).appendTo($(this.wnd).find(".chat-msgs"));
    }

    height = $(this.wnd).find(".chat-msgs")[0].scrollHeight;
    console.log(height);
    $(this.wnd).find(".chat-msgs").animate({
        scrollTop: height
    }, 'slow');
}


function chat(options) {

    var pthis = this;

    this.me = options.me;
    this.socket = options.socket;
    this.users = new Array();
    this.chatwnds = new Array();

    this.addressWindow = new CAddressWindow();
    this.addressWindow.init();

    $(this.addressWindow).on("openchat", function (evt, uid) {
        pthis.openChatWindow(uid)
    });

    this.addListeners();

    this.socket.emit('join', this.me);
    this.socket.emit('get users', this.me);
}

chat.prototype.openChatWindow = function (uid) {

    var pthis = this;

    if (this.chatwnds[uid] != null && this.chatwnds[uid] != undefined) {
        console.log("window already opened!");
        this.chatwnds[uid].maximizeWindow();
        return;
    }

    var newwnd = new CChatWindow(this.users[uid]);

    var rpos = countArray(this.chatwnds) * 210 + 300;
    newwnd.openWindow(rpos);

    $(newwnd).on("close", function (evt, uid) {
        console.log(pthis.chatwnds);
        delete pthis.chatwnds[uid];
        console.log(pthis.chatwnds);
    });

    $(newwnd).on("sendmessage", function (evt, msg) {
        console.log("[trigger sendmessage]");
        console.log(msg);

        msg["sender"] = pthis.me;
        pthis.chatwnds[msg.receiver.id].addMessage(msg);
        pthis.socket.emit("send message", msg);
    });

    this.chatwnds[uid] = newwnd;

}

chat.prototype.addListeners = function () {

    var pthis = this;

    this.socket.on('get users', function (users) {

        console.log("[get users]");
        console.log(users);

        pthis.addressWindow.setUserList(pthis.me, users);

        pthis.users = users;
    });

    this.socket.on('join', function (new_user) {

        console.log("[join]");
        console.log(new_user);

        pthis.addressWindow.addUser(new_user);

        pthis.users[new_user.id] = new_user;
    });

    this.socket.on('quit', function (new_user) {
    
        console.log("[quit]");
        console.log(new_user);

        pthis.addressWindow.removeUser(new_user);
        pthis.chatwnds[new_user.id].closeWindow();

        delete pthis.chatwnds[new_user.id];
        delete pthis.users[new_user.id];

        console.log(pthis.users);
    });

    this.socket.on('send message', function (msg) {

        console.log("[send message]");
        console.log(msg);

        pthis.chatwnds[msg.sender.id].addMessage(msg);
    });
}

function countArray(src) {
    if (src == null || src == undefined)
        return 0;

    i = 0;
    for (p in src) {
        i++;
    }

    return i;
}