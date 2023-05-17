$(document).ready(function() {
    $("#yes").hide(true);
    $("#yes1").hide(true);
    $("#yes2").hide(true);
    $("#no").hide(true);
    $("#lblAvis").hide(true);
    $('#nif').prop('readonly', true);
    $('#username').prop('readonly', true);
    $('#birthdate').prop('readonly', true);
    $('#collegiate_uid').prop('readonly', true);
    $('#speciality').prop('readonly', true);
    


    $("#cancel").click(function(ev){
        ev.preventDefault();
        $("#lblAvis").show(true);
        $("#yes").show(true);
        $("#no").show(true);
    })
    $("#update").click(function(ev){
        ev.preventDefault();
        $("#lblAvis").show(true);
        $("#yes1").show(true);
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
