$(document).ready(function() {
    $("#miElemento").hide(true);
    $("#cancel").hide(true);
    $("#update").hide(true);
    $("#remove").hide(true);
    $("#yes").hide(true);
    $("#yes1").hide(true);
    $("#yes2").hide(true);
    $("#no").hide(true);
    $("#lblAvis").hide(true);
    $('#nif').prop('readonly', false);
    $('#username').prop('readonly', false);
    $('#birthdate').prop('readonly', false);

    $('#patientsTable tr').click(function() {
        $("#cancel").show(true);
        $("#update").show(true);
        $("#remove").show(true);
        $("#add").hide(true);
        $('#nif').prop('readonly', true);
        $('#username').prop('readonly', true);
        $('#birthdate').prop('readonly', true);

        $('#patientsTable tr').each(function(){
            $(this).css("background-color", "white");
        });
        $(this).css("background-color", "#00D5CE");
        let id=$(this).find("input").val();
        $("#patientSelect").val(id);
        $.ajax({
          type: 'GET',
          url: 'doctor/putInfo?patient='+id,
          success: function(info) {
            $('#first_name').val(info.info[0].pers_first_name);
            $('#last_name').val(info.info[0].pers_last_name_1);
            $('#last_name1').val(info.info[0].pers_last_name_2);
            $('#nif').val(info.info[0].pers_nif);
            $('#phone').val(info.info[0].pers_phone_number);
            $('#email').val(info.info[0].pers_email);
            $('#birthdate').val(info.info[0].pers_birthdate);
            $('#gender').val(info.info[0].pers_gender);
            $('#street').val(info.info[0].pers_addrs_street);
            $('#caregiver').val(info.info[0].pati_caregiver_id);
            $("#postal_code").val(info.info[0].pers_addrs_zip_code);
            $("#city").val(info.info[0].pers_addrs_city);
            $("#province").val(info.info[0].pers_addrs_province);
            $("#country").val(info.info[0].pers_addrs_country);
            $("#username").val(info.info[0].pers_login_username);
            $("#weigth").val(info.info[0].pati_weight);
            $("#heigth").val(info.info[0].pati_height);
            $("#remarks").val(info.info[0].pati_remarks);
          }
        });
        $.ajax({
            type: 'GET',
            url: 'doctor/putTreatments?patient='+id,
            success: function(info) {
                let table = $('#treatmentsTable tbody');
                table.empty();
                for (let i = 0; i < info.treatments.length; i++) {
                    let treatment = info.treatments[i];
                    let row = $('<tr></tr>');
                    row.append('<td>' + treatment.trea_name + '</td>');
                    row.append('<td>' + treatment.trea_date_start + '</td>');
                    row.append('<td>' + treatment.trea_date_end + '</td>');
                    row.append('<td>' + (treatment.trea_is_active="0"?"Active":"No Active") + '</td>');
                    table.append(row);
                }
            }
          });
      });

    $("#cancel").click(function(ev){
        ev.preventDefault();
        $("#lblAvis").show(true);
        $("#yes").show(true);
        $("#no").show(true);
    })
    $("#update").click(function(ev){
        ev.preventDefault();
        let ok= validForm();
        if(ok){
            $("#lblAvis").show(true);
            $("#yes1").show(true);
            $("#no").show(true);
        }
    })
    $("#remove").click(function(ev){
        ev.preventDefault();
        $("#lblAvis").show(true);
        $("#yes2").show(true);
        $("#no").show(true);
    })
    $("#no").click(function(ev){
        ev.preventDefault();
        $("#lblAvis").hide(true);
        $("#yes").hide(true);
        $("#yes1").hide(true);
        $("#yes2").hide(true);
        $("#no").hide(true);
    })

});
function validForm() {
    let errors = []; 
    let first_name = $('#first_name').val();
    if (first_name.length < 2) {
      errors.push('First name must have 2 characters or more.');
    }
    let last_name = $('#last_name').val();
    if (last_name.length < 2) {
      errors.push('Last name must have 2 characters or more.');
    } 
    let phone = $('#phone').val();
    let phone_regex = /^[0-9]{9}$/;
    if (!phone_regex.test(phone)) {
      errors.push('Error format in phone.');
    }
    let email = $('#email').val();
    let email_regex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
    if (!email_regex.test(email)) {
      errors.push('Error format in email.');
    }
    let gender = $('#gender').val();
    let gender_regex = /^[0-3]{1}$/;
    if (!gender_regex.test(gender)) {
      errors.push('Gender is required.');
    }
    let street = $('#street').val();
    if (street.length < 10) {
      errors.push('Street must have 10 characters or more.');
    }
    let postal_code = $('#postal_code').val();
    let postal_code_regex = /^[0-9]{5}$/;
    if (!postal_code_regex.test(postal_code)) {
      errors.push('Error format in postal code.');
    }
    let city = $('#city').val();
    if (city.length < 3) {
      errors.push('City must have 3 characters or more.');
    }
    let province = $('#province').val();
    if (province.length < 3) {
      errors.push('Province must have 3 characters or more.');
    }
    let country = $('#country').val();
    if (country.length < 3) {
      errors.push('Country must have 3 characters or more.');
    }
    let weigth = $('#weigth').val();
    if (weigth<2 || weigth>400) {
        errors.push('Weigth must be between 2-400.');
    }
    let heigth = $('#heigth').val();
    if (heigth<30 || heigth>260) {
        errors.push('Heigth must be between 30-260.');
    } 
    if (errors.length > 0) {   
        let modal = $('#errorModal');
        let errorList = $('#errorList');
        errorList.empty(); 
        for (let i = 0; i < errors.length; i++) {
          errorList.append('<li>' + errors[i] + '</li>');
        }
        modal.show();
        $('#closeModal').click(function(ev) {
            ev.preventDefault();
            modal.hide();
        });
        return false;
    } else {
        return true;
    }

}  