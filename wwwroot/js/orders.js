$(document).ready(function() {
    let client = $("#ClientId");
    let sortableContainer = $("#sortable");

    client.on("change", function() {
        $.ajax({
            url: RootUrl + "Projects/GetProjects/",
            data: { id: client.val() },
            type: "GET",
            success: function (data) {
                sortableContainer.empty();
                if (data.status && data.status === "not_found") {
                    var sortableItem = $("<div>", {
                        class: "",
                        text: "Brak korków dla wybranego projektu"
                    });
                    sortableContainer.append(sortableItem);
                } else {
                    // sort data
                    data.sort(function (a, b) {
                        return a.order - b.order;
                    });
                    sortableContainer.empty();
                    $.each(data, function (index, project) {
                        var sortableItem = $("<div>", {
                            class: "sortable-item",
                            "data-id": project.projectId,
                            text: project.name // + " (order: " + unit.order + ")"
                        });
                        sortableContainer.append(sortableItem);
                    });
                }
            },
            error: function () {
                sweetAlert("Coś poszło nie tak...", "Sprawdź czy połączenie dla Projektu zostało odpowiednio zdefiniowane w Administracji", "warning");
            }
        });
    });

    $(function() {
        $("#sortable").sortable({
            update: function (event, ui) {
                let stepsIds = [];
                $(".sortable-item").each(function () {
                    stepsIds.push($(this).data("id"));
                });
                let clientId = $("#ClientId").val();
                $.ajax({
                    url: RootUrl + "Projects/UpdateOrder/",
                    //url: "@Url.Action("UpdateOrder", "UnitSteps")",
                    type: "POST",
                    data: { stepsIds: stepsIds, clientId: clientId },
                    traditional: true,
                    success: function () {
                    },
                    error: function () {
                    }
                });
            }
        });
    });
});