$(document).ready(function () {

    /**
     * This function will pass the userId and current
     * userAccess and update it on change in database
     */
    $(".access-change").change(function () {
        let new_access = $(this).val();
        let user = $(this).closest("tr").find(".user-id").text();
        $.ajax({
            type: "POST",
            url: RootUrl + "Users/ChangeAccessLevel/",
            headers: {
                RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            },
            data: { userId: user, userAccess: new_access },
            success: function (response) {
                if (response.success) {
                    console.log("success");
                }
                else {
                    console.log("error");
                }
            },
            error: function (xhr, status, error) {
                console.log(error);
                console.log(xhr.responseText);
            },
        });
    });
});
