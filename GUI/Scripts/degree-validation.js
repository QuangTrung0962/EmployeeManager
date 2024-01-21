
document.addEventListener("DOMContentLoaded", function () {
    let releaseDateInput = document.getElementById("releaseDate");
    let expirationDateInput = document.getElementById("expirationDate");

    releaseDateInput.addEventListener("blur", function () {
        validateDates();
    });

    expirationDateInput.addEventListener("blur", function () {
        validateDates();
    });

    function validateDates() {
        let releaseDateValue = releaseDateInput.value;
        let expirationDateValue = expirationDateInput.value;

        if (isNaN(Date.parse(releaseDateValue)) || new Date(releaseDateValue).getFullYear() <= 1990 || new Date(releaseDateValue) > new Date()) {
            document.getElementById("releaseDate-error").innerText = "Ngày phát hành phải là một năm lớn hơn 1990 và nhỏ hơn năm hiện tại";
            releaseDateInput.value = "";
            releaseDateInput.focus();
            return;
        } else {
            document.getElementById("releaseDate-error").innerText = "";
        }

        if (isNaN(Date.parse(expirationDateValue)) || new Date(releaseDateValue) > new Date()) {
            document.getElementById("expirationDate-error").innerText = "Ngày hết hạn không hợp lệ.";
            expirationDateInput.value = "";
            expirationDateInput.focus();
            return;
        } else {
            document.getElementById("expirationDate-error").innerText = "";
        }


        if (new Date(releaseDateValue) >= new Date(expirationDateValue)) {
            document.getElementById("releaseDate-error").innerText = "Ngày phát hành phải nhỏ hơn ngày hết hạn.";
            releaseDateInput.value = "";
            expirationDateInput.value = "";
            releaseDateInput.focus();
        } else {
            document.getElementById("releaseDate-error").innerText = "";
        }
    }
});
