$(function () {
    let users = obtain_users();
    render_users(users);

    $(".name").change(function () {
        let user = {
            id: $(this).data("id"),
            name: $(this).val(),
            cashBalance: parseInt($(this).parent("td").next("td").find(".cashBalance").val())
        }
        update_user(JSON.stringify(user));

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
        view += "<td><input class=\"cashBalance\" type=\"text\" value=\"" + user.cashBalance + "\" data-id=\"" + user.id +"\" /></td>";
        view += "</tr>";
    });
    $("#users tr").next().remove();
    $("#users tr").after(view);
}

function update_user(user) {
    $.ajax({
        type: 'POST',
        dataType: 'json',
        async: false,
        url: '/User/update_user',
        contentType: "application/json; charset=utf-8",
        data: user
    });
}