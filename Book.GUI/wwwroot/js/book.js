
var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url": "/Admin/Book/GetAll"
        },
        "columns": [
            { "data": "isbn", "width": "10%" },
            { "data": "title", "width": "15%" },
            { "data": "edition", "width": "10%" },
            { "data": "price", "width": "10%" },
            { "data": "availableQuantity", "width": "10%" },
            { "data": "bookAuthors[ ].author.fullName", "width": "10%" },
            { "data": "bookGenres[ ].genre.name", "width": "10%" },
            { "data": "publisher.publisherName", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/Book/Upsert/${data}" class="btn btn-success text-white"><i class="fas fa-edit"></i></a>
                                <a onclick='Delete("/Admin/Book/Delete/${data}")' class="btn btn-danger text-white"><i class="fas fa-trash-alt"></i></a>
                            </div>
                            `
                },
                "width": "15%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}