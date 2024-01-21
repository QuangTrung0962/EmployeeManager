let dobInput = document.getElementById('dob');
let ageDisplay = document.getElementById('age');

dobInput.addEventListener('input', function () {
    let dob = new Date(this.value);
    let today = new Date();
    let age = today.getFullYear() - dob.getFullYear();

    //Vì dob đang ở dạng dd/MM/yyyy
    if (today.getMonth() < dob.getDate() || (today.getMonth() === dob.getDate() && today.getDate() < dob.getMonth())) {
        age--;
    }

    if (age < 0) age = "";

    ageDisplay.value = age;
});


document.addEventListener('DOMContentLoaded', function () {
    let phoneNumberCheckbox = document.getElementById('not-phonenumber');
    let phoneNumberInput = document.getElementById('phone-number');

    let idCardCheckbox = document.getElementById('not-idcard');
    let idCardInput = document.getElementById('id-card');

    phoneNumberCheckbox.addEventListener('click', function () {
        if (phoneNumberCheckbox.checked) {
            phoneNumberInput.readOnly = true;
            phoneNumberInput.value = '';
        } else {
            phoneNumberInput.readOnly = false;
        }
    });

    idCardCheckbox.addEventListener('click', function () {
        if (idCardCheckbox.checked) {
            idCardInput.readOnly = true;
            idCardInput.value = '';
        } else {
            idCardInput.readOnly = false;
        }
    });
});

