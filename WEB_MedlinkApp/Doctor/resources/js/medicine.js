$(document).ready(function() {
    $("#cancel").hide("true");
    $("#add").hide("true");
    $("#update").hide("true");
    $("#remove").hide("true");
    $("#yes").hide("true");
    $("#yes1").hide("true");
    $("#yes2").hide("true");
    $("#yes3").hide("true");
    $("#no").hide("true");
    $("#lblAvis").hide("true");

    $('#medicinesTable tr').click(function() {
      $("#add").hide("true");
      $("#cancel").show("true");
      $("#update").show("true");
      $("#remove").show("true");
  
      $('#medicinesTable tr').each(function(){
        $(this).css("background-color", "white");
      });
      $('#tableMedicines tr').each(function(){
        $(this).css("background-color", "white");
      });
      $(this).css("background-color", "#00D5CE");
      let id=$(this).find("input").val();
      $("#mediId").val(id);
      let nom = $(this).find('td:first').text();
      $.ajax({
        type: 'GET',
        url: 'getMedicineTreat?medi='+id,
        success: function(info) {
          $('#end').val(info.info[0].trme_date_end);
          $('#start').val(info.info[0].trme_date_start);
          $('#total').val(info.info[0].trme_total_quantity);
          $('#unit').val(info.info[0].trme_unit_of_measure_id);
          $("#selectedMedicine").val(nom);        
        }
      });

    });

    $('#tableMedicines tr').click(function() {
        $("#add").show("true");
        $("#cancel").hide("true");
        $("#update").hide("true");
        $("#remove").hide("true");
    
        let nom = $(this).find('td:first').text();
        $("#selectedMedicine").val(nom);
        $('#end').val("");
        $('#start').val("");
        $('#total').val("");
        $('#unit').val("");
        // $("#cancel").show("true");
        // $("#update").show("true");
        // $("#remove").show("true");
        // $("#treatmentDetails").show("true");
        // $("#add").hide("true");
        // $("#medicineDetails").show("true");


        $('#tableMedicines tr').each(function(){
            $(this).css("background-color", "white");
        });

        $('#medicinesTable tr').each(function(){
          $(this).css("background-color", "white");
        });

        $(this).css("background-color", "#00D5CE");
        let id=$(this).find("input").val();
        $("#mediId").val(id);


        // $.ajax({
        //     type: 'GET',
        //     url: 'putMedicines?treat='+id,
        //     success: function(info) {
        //       let table = $('#medicinesTable tbody');
        //       table.empty();
        //       console.info(info);
        //       for (let i = 0; i < info.medicines.length; i++) {
        //           let medicine = info.medicines[i];      
        //           let row = $('<tr>');
        //           row.append($('<td>').text(medicine.medi_name));
        //           row.append($('<td>').text(medicine.meca_name));
        //           row.append($('<td>').text(medicine.trme_quantity_per_day));
        //           row.append($('<td>').text(medicine.unme_name));
        //           table.append(row);
        //       }
      
        //     }
        //   });
      });
      $("#add").click(function(ev){
        ev.preventDefault();
        $.ajax({
          type: 'GET',
          url: 'getTreatment',
          success: function(info) {
            let end=info.info[0].trea_date_end;
            let start=info.info[0].trea_date_start;
            let ok=validForm(end,start);
            if(ok){
              $("#lblAvis").show("true");
              $("#yes3").show("true");
              $("#no").show("true");
            }
          }
        });
      });

    $("#cancel").click(function(ev){
        ev.preventDefault();
        $("#lblAvis").show("true");
        $("#yes").show("true");
        $("#no").show("true");
    });

    $("#update").click(function(ev){
        ev.preventDefault();
        $.ajax({
          type: 'GET',
          url: 'getTreatment',
          success: function(info) {
            let end=info.info[0].trea_date_end;
            let start=info.info[0].trea_date_start;
            let ok=validForm(end,start);
            if(ok){
              $("#lblAvis").show("true");
              $("#yes1").show("true");
              $("#no").show("true");
            }
          }
        });
    });
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
        $("#yes3").hide("true");
        $("#no").hide("true");
    })

});
function validForm(end, start) {
    let errors = []; 
    let startDate = $('#start').val();
    let endDate = $('#end').val();
    let qtat=$('#total').val();
    let unit=$('#unit').val();
    let now = new Date();
    if (endDate=="" || end < endDate) {
      errors.push('End date medicine is required and must be between dates of treatment.');
    }

    if (startDate=="" || start > startDate) {
      errors.push('Start date medicine is required and must be between dates of treatment.');
    }

    if(startDate<now){
      errors.push('Start Date must be future.');
    }

    if(startDate>endDate){
      errors.push('Start Date must before than End Date.');
    }

    if (qtat=="" || isNaN(qtat)) {
      errors.push('Total Qtat is required and must be a number.');
    }
    if(unit==-1){
      errors.push('Unit is required.');
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