$(document).ready(function() {
    $("#frmSignup").form({
        fields: {
            account_name: {
                name: "account_name",
                rules: [
                    { type: 'regExp[/^[a-z0-9_-]{4,16}$/]', prompt: 'Your account name should be<br> 4~16 length<br>Only a-z, 0-9, _, -.' },
                ],
                inline:true
            },
            email: { name: "email", rules: [{ type: 'email', prompt: 'Input a correct email.' }], inline: true },
            pwd: { name: "pwd", rules: [{ type: 'minLength[6]', prompt: 'Input the password more than 6 chars' }], inline: true },
            retype_pwd: { name: "retype_pwd", rules: [{ type: 'match[pwd]', prompt: 'Retype the password. It should be equal with password.' }], inline: true }
        },
        inline:true
    });

    $("#frmSignup").submit(function () {

        f = $("#frmSignup").form("is valid");

        return f;
    });
});