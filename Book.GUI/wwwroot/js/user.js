//import { type } from "jquery";

var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url": "/Admin/User/GetAll"
        },
        "columns": [
            { "data": "fullName", "width": "20%" },
            { "data": "phoneNumber", "width": "10%" },
            { "data": "email", "width": "15%" },
            { "data": "address", "width": "25%" },
            { "data": "role", "width": "10%" },
            {
                "data": { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        return `
                            <div class="text-center">
                                <a onclick='LockOrUnlock("${data.id}")' class="btn btn-success text-white" style="width:100px;">
                                    <i class="fas fa-lock-open"></i> Unlock
                                </a>
                            </div>
                            `;
                    }
                    else {
                        return `
                            <div class="text-center">
                                <a onclick='LockOrUnlock("${data.id}")' class="btn btn-danger text-white" style="width:100px;">
                                    <i class="fas fa-lock"></i> Lock
                                </a>
                            </div>
                            `;
                    }
                },
                "width": "10%"
            }
        ]
    });
}

function LockOrUnlock(id) {
    $.ajax({
        type: "POST",
        url: "/Admin/User/LockOrUnlock",
        data: JSON.stringify(id),
        contentType:"application/json",
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
