﻿//import { type } from "jquery";

var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url": "/Admin/Publisher/GetAll"
        },
        "columns": [
            { "data": "publisherName", "width": "60%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/Publisher/Upsert/${data}" class="btn btn-success text-white"><i class="fas fa-edit"></i></a>
                                <a onclick='Delete("/Admin/Publisher/Delete/${data}")' class="btn btn-danger text-white"><i class="fas fa-trash-alt"></i></a>
                            </div>
                            `
                },
                "width": "40%"
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