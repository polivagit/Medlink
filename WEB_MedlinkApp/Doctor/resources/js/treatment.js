$(document).ready(function() {
    $("#cancel").hide(true);
    $("#update").hide(true);
    $("#remove").hide(true);
    $("#yes").hide(true);
    $("#yes1").hide(true);
    $("#yes2").hide(true);
    $("#no").hide(true);
    $("#treatmentDetails").hide(true);
    $("#lblAvis").hide(true);
    let currentDate = new Date().toISOString().slice(0, 10);
    $('#start').val(currentDate);
    $('#start').prop('readonly', true);

    $('#treatmentsTable1 tr').click(function() {
        $("#cancel").show(true);
        $("#update").show(true);
        $("#remove").show(true);
        $("#treatmentDetails").show(true);
        $("#add").hide(true);

        $('#treatmentsTable1 tr').each(function(){
            $(this).css("background-color", "white");
        });
        $(this).css("background-color", "#00D5CE");
        let id=$(this).find("input").val();
        $("#treatSelect").val(id);

        $.ajax({
          type: 'GET',
          url: 'putInfo?treat='+id,
          success: function(info) {
            $('#name').val(info.info[0].trea_name);
            $('#active').val(info.info[0].trea_is_active);
            $('#start').val(info.info[0].trea_date_start);
            $('#end').val(info.info[0].trea_date_end);
            $('#desc').val(info.info[0].trea_description);
            $('#obs').val(info.info[0].trea_observations);

          }
        });
        $.ajax({
            type: 'GET',
            url: 'putMedicines?treat='+id,
            success: function(info) {
              let table = $('#medicinesTable tbody');
              table.empty();
              console.info(info);
              for (let i = 0; i < info.medicines.length; i++) {
                  let medicine = info.medicines[i];      
                  let row = $('<tr>');
                  row.append($('<td>').text(medicine.medi_name));
                  row.append($('<td>').text(medicine.meca_name));
                  row.append($('<td>').text(medicine.trme_quantity_per_day));
                  row.append($('<td>').text(medicine.unme_name));
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