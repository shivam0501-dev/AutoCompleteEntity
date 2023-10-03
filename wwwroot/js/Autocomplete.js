$(document).ready(function () {
    alert("ok");
});
    function AutoComplete(Id, GetType) {
        $('#' + Id).autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Home/AutoComplete",
                    type: "POST",
                    dataType: "json",
                    data: { prefix: request.term, type: GetType },
                    success: function (data) {
                        debugger
                        response($.map(data, function (item) {
                            return { label: item.countryName, value: item.countryId };
                        }))
                    }
                })
            },
            messages: {
                noResults: "", results: ""
            }
        });
}

