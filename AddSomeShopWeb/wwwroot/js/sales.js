$(document).ready(function () {
    $('#productSearch').select2({
        ajax: {
            url: '/Admin/POS/GetProducts', // Update the URL to the GetProducts action
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    term: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data
                };
            },
            cache: true
        },
        placeholder: 'Search product...',
        minimumInputLength: 1,
        templateResult: formatResults
    });

    // Add an event handler for 'select2:select' event
    $('#productSearch').on('select2:select', function (e) {
        var data = e.params.data;

        // Display a SweetAlert
        swal({
            title: 'You selected:',
            text: data.text,
            type: 'success'
        });
    });

});

function formatResults(data) {
    if (data.loading)
        return data.text;

    var imageUrl = data.img; // Assuming data.img contains the relative image path from the wwwroot folder

    // Construct the complete image URL by prepending the application base URL
    var completeImageUrl = window.location.origin + imageUrl;

    var container = $(
        `<table width="100%">
            <tr>
                <td style="width:60px">
                    <img style="height:60px;width:60px;margin-right:10px" src="${completeImageUrl}"/>
                </td>
                <td>
                    <p style="font-weight: bolder;margin:2px">${data.text}</p>
                    <p style="margin:2px">${data.retailPrice}</p>
                </td>
            </tr>
         </table>`
    );

    return container;
}


$(document).on('select2:open', () => {
    document.querySelector('.select2-search__field').focus();
});


