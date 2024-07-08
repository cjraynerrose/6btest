$(document).ready(function () {
    document.getElementById("ApproveAppointmentBtn").addEventListener("click", function (e) {
        var token = document.querySelector("[name='__RequestVerificationToken']").value;
        var id = this.attributes["data-id"].value;
        var formData = new FormData();
        formData.append("__RequestVerificationToken", token);
        formData.append("id", id);

        fetch("/Admin/ApproveAppointment", {
            method: "POST",
            body: formData
        })
            .then(response => {
                if (response.ok) {
                    location.reload();
                } else {
                    alert("An error occurred while approving the appointment")
                }
            })
    });
});