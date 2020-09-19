$(function () {
    let users = obtain_users();
    render_users(users);

    $(".add").click(function () {
        let user = {
            name: '',
            cashBalance: 0
        }
        post_api("/User/add_user", JSON.stringify(user));

        users = obtain_users();
        render_users(users);
    });

    $(document).on("click", ".save", function () {
        let user = {
            id: $(this).data("id"),
            name: $(this).parent("td").prev("td").prev("td").find(".name").val(),
            cashBalance: parseInt($(this).parent("td").prev("td").find(".cashBalance").val())
        }
        post_api("/User/update_user", JSON.stringify(user));

        users = obtain_users();
        render_users(users);
    });

    $(document).on("click", ".delete", function () {
        let user = {
            id: $(this).data("id")
        }
        post_api("/User/delete_user", JSON.stringify(user));

        users = obtain_users();
        render_users(users);
    });
});

function obtain_users() {
    let return_data = [];
    $.ajax({
        type: 'GET',
        dataType: 'json',
        async: false,
        url: '/User/obtain_users',
        success: function (msg) {
            return_data = msg;
        }
    });
    return return_data;
}

function render_users(users) {
    let view = "";
    users.forEach(function (user) {
        view += "<tr>";
        view += "<td><input class=\"name\" type=\"text\" value=\""+user.name+"\" data-id=\""+user.id+"\" /></td>";
        view += "<td><input class=\"cashBalance\" type=\"text\" value=\"" + user.cashBalance + "\" data-id=\"" + user.id + "\" /></td>";
        view += "<td>";
        view += "<input class=\"save\" type=\"button\" value=\"儲存\" data-id=\"" + user.id + "\" />";
        view += "<input class=\"delete\" type=\"button\" value=\"刪除\" data-id=\"" + user.id + "\" />";
        view += "</td > ";
        view += "</tr>";
    });
    $("#users tr").next().remove();
    $("#users tr").after(view);
}

function post_api(url, data) {
    $.ajax({
        type: 'post',
        dataType: 'json',
        async: false,
        url: url,
        contentType: "application/json; charset=utf-8",
        data: data
    });
}