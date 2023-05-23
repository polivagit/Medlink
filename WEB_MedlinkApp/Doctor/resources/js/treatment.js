$(document).ready(function() {
    $("#cancel").hide("true");
    $("#update").hide("true");
    $("#remove").hide("true");
    $("#yes").hide("true");
    $("#yes1").hide("true");
    $("#yes2").hide("true");
    $("#no").hide("true");
    $("#treatmentDetails").hide("true");
    $("#lblAvis").hide("true");
    $("#medicineDetails").hide("true");
    let currentDate = new Date().toISOString().slice(0, 10);
    $('#start').val(currentDate);
    $('#start').prop('readonly', "true");

    $('#treatmentsTable1 tr').click(function() {
        $("#cancel").show("true");
        $("#update").show("true");
        $("#remove").show("true");
        $("#treatmentDetails").show("true");
        $("#add").hide("true");
        $("#medicineDetails").show("true");


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
        $("#lblAvis").show("true");
        $("#yes").show("true");
        $("#no").show("true");
    })
    $("#update").click(function(ev){
        ev.preventDefault();
        let ok= validForm();
        if(ok){
            $("#lblAvis").show("true");
            $("#yes1").show("true");
            $("#no").show("true");
        }
    })
    $("#remove").click(function(ev){
        ev.preventDefault();
        $("#lblAvis").show("true");
        $("#yes2").show("true");
        $("#no").show("true");
    })
    $("#no").click(function(ev){
        ev.preventDefault();
        $("#lblAvis").hide("true");
        $("#yes").hide("true");
        $("#yes1").hide("true");
        $("#yes2").hide("true");
        $("#no").hide("true");
    })

});
function validForm() {
    let errors = []; 
    let name = $('#name').val();
    if (name.length < 2) {
      errors.push('Name must have 2 characters or more.');
    }
    let active = $('#active').val();
    if (!(active == 0 || active == 1)) {
      errors.push('Is Active is required.');
    } 
    let end = $('#end').val();
    let now = new Date();
    let end2=new Date(end);
    if (end2<now) {
      errors.push('End Date must be future.');
    }
    let email = $('#desc').val();
    if (email.length < 10) {
      errors.push('Description must have 10 characters or more.');
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