$(document).ready(function () {
    $("#frmSignin").form({
        fields: {
            account_name: {
                name: "account_name",
                rules: [
                    { type: 'empty', prompt: 'Please input account name.' },
                ],
                inline: true
            },
            pwd: { name: "pwd", rules: [{ type: 'minLength[6]', prompt: 'Input the password more than 6 chars' }], inline: true }
        },
        inline: true
    });

    $("#frmSignin").submit(function () {

        f = $("#frmSignin").form("is valid");

        return f;
    });
});