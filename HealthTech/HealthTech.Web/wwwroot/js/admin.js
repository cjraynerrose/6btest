$(document).ready(function () {
    document.getElementById("ApproveAppointmentBtn").addEventListener("click", function (e) {
        var token = document.querySelector("[name='__RequestVerificationToken']").val();
        var id = this.id;
        var formData = new FormData();
        formData.append("__RequestVerificationToken", token);
        formData.append("id", id);

        fetch("/Admin/ApproveAppointment", {
            method: "POST",
            body: formData
        })
    });
});