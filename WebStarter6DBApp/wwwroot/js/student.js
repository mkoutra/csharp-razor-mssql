function confirmDelete(studentId) {
    if (confirm("Are you sure you want to delete student with id " + studentId + "?")) {
        window.location.href = "/Students/" + studentId + "/Delete";
    }
}