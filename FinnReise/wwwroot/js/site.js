// Jquery for å vise dato kalender. 
var dateToday = new Date();
$(function () {
    $("#datepicker").datepicker({minDate: dateToday, maxDate: '+3M'}).datepicker("setDate", new Date());
});

$(function () {
    $("#datepicker-tur").datepicker({minDate: dateToday, maxDate: '+3M'}).datepicker("setDate", new Date());
});

// JQuery plugin for å vise tidvelger. 
$(function () {
    $("#timepicker").timepicker({
        'step': 60,
        'show2400': true,
        'timeFormat': 'H:i',
        disableTextInput: true
    }).timepicker('setTime', new Date());
});
var d1 = new Date();

var d2 = new Date(d1);
d2.setHours(d1.getHours() + 1)

$(function () {
    $("#timepicker-tur").timepicker({
        'step': 60,
        'show2400': true,
        'timeFormat': 'H:i',
        disableTextInput: true
    }).timepicker('setTime', d2);
});

// For å vise Retur div'en når Tur/Retur er valgt.
$(document).ready(function () {

    $('input[type="radio"]').click(function () {
        if ($(this).attr("value") == "EnVei") {
            $(".retur").hide();
        }
        if ($(this).attr("value") == "TurRetur") {
            $(".retur").show();

        }
    });

        $('input[id$="en-vei"]').trigger('click');  // trigger the event

});



// Sjekker at man ikke kan ta å skrive inn noe annet enn tall ved å bruke input filter.

(function ($) {
    $.fn.inputFilter = function (inputFilter) {
        return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function () {
            if (inputFilter(this.value)) {
                this.gammelVerdi = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            } else if (this.hasOwnProperty("gammelVerdi")) {
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            }
        });
    };
}(jQuery));

$(document).ready(function () {
    // Forhindrer at man kan taste inn noe annet en tall i antall input
    $("#AntallVoksen").inputFilter(function (value) {
        return /^\d*$/.test(value);
    });
});

// Hvis det ikke er en verdi i input, legg automatisk til 0.
$('#AntallVoksen').on('change blur', function () {
    if ($(this).val().trim().length === 0) {
        $(this).val(0);
    }
})

// Endrer første bokstav til stor når man begynner å skrive
$('#FraStasjon, #TilStasjon').keyup(function () {
    if ($(this).val().length > 0 && $(this).val().length < 5) {
        $(this).val($(this).val().charAt(0).toUpperCase() + $(this).val().substr(1));
    }
});

// Bytt verdi på Fra Stasjon og til Stasjon når man trykker på en knapp
$('.byttVerdi').click(function (event) {
    event.preventDefault();
    var fraStasjon = $('#FraStasjon').val();
    var tilStasjon = $('#TilStasjon').val();
    if (fraStasjon != "" && tilStasjon != "") {
        $('#FraStasjon').val(tilStasjon);
        $('#TilStasjon').val(fraStasjon);

    }

});


// Gjør at byttVerdiKnapp blir disabled når det ikke er skrevet noe i inputs
$(document).ready(function() {
    $('.stasjon input').keyup(function() {

        var empty = false;
        $('.stasjon input').each(function() {
            if ($(this).val().length == 0) {
                empty = true;
            }
        });

        if (empty) {
            $('.byttVerdi button').attr('disabled', 'disabled');
        } else {
            $('.byttVerdi button').attr('disabled', false);
        }
    });
});



