// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    // make people (faculty / staff) into tabs
    $('#peopleTabs').tabs();

    // make employment tables into tabs
    $('.tables').tabs();

    $('#minorsAcc').accordion({
        heightStyle: "content",
        collapsible: true,
        active: true
    });
    $('.coursesAcc').accordion({
        heightStyle: "content",
        collapsible: true,
        active: true
    });

    new DataTable('#coopTable');
    new DataTable('#profTable');

    //turns everything on
    $('#allDegrees').fadeIn(1000);
    $('#minorsAcc').fadeIn(1000);
    $('.tables').fadeIn(1000);
    $('#allPeople').fadeIn(1000);

});