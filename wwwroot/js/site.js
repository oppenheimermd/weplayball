
var toggled = true;

// A $( document ).ready() block.
$(document).ready(function () {

    

    // Sidebar Toggler
    function sidebarToggle() {
        console.log("sidebarToggle() called");
        var sidebar = $('#sidebar');
        var padder = $('.content-padder');

        console.log("value of toggle in sideBarToggle() enter :" + toggled);

        if (toggled) {

            sidebar.css({ 'display': 'block', 'x': -300 });
            sidebar.transition({ opacity: 1, x: 0 }, 250, 'in-out', function () {
                sidebar.css('display', 'block');
            });
            if ($(window).width() > 960) {
                padder.transition({ marginLeft: sidebar.css('width') }, 250, 'in-out');

                console.log("window width: " + $(window).width());
            }

            //  Debug
            console.log("value of toggle in sideBarToggle() exit:" + toggled);

        } else {
            console.log("sidebarToggle else called!");
            sidebar.css({ 'display': 'block', 'x': '0px' });
            sidebar.transition({ x: -300, opacity: 0 }, 250, 'in-out', function () {
                sidebar.css('display', 'none');
            });
            padder.transition({ marginLeft: 0 }, 250, 'in-out');


            console.log("value of toggle in sideBarToggle() exit :" + toggled);
        }
    }

    $('#sidebar_toggle').click(function () {
        var sidebar = $('#sidebar');

        console.log("value of toggle in click() entry:" + toggled);

        if (sidebar.css('x') == '-300px' || sidebar.css('display') === 'none') {
            //sidebarToggle(true);
            toggled = false;
            sidebarToggle();
        } else {
            //sidebarToggle(false);
            toggled = true;
            sidebarToggle();

            console.log("value of toggle in click() exit:" + toggled);
        }
    });

    function resize() {
        var sidebar = $('#sidebar');
        var padder = $('.content-padder');
        padder.removeAttr('style');
        if ($(window).width() < 960 && sidebar.css('display') === 'block') {
            //sidebarToggle(false);
            toggled = true;
            sidebarToggle();
            console.log("window width: " + $(window).width());
        } else if ($(window).width() > 960 && sidebar.css('display') === 'none') {
            //sidebarToggle(true);
            toggled = false;
            sidebarToggle();
            console.log("window width: " + $(window).width());
        }
    }

    if ($(window).width() < 960) {
        //sidebarToggle(false);
        toggled = true;
        sidebarToggle();
        console.log("window width: " + $(window).width());
    }

    $(window).resize(function () {
        resize();
        console.log("window width: " + $(window).width());
    });

    $('.content-padder').click(function () {
        if ($(window).width() < 960) {
            //sidebarToggle(false);
            toggled = true;
            sidebarToggle();
            console.log("window width: " + $(window).width());
        }
    });
});

