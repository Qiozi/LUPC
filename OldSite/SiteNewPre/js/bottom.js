$(document).ready(function () {
    $('#page-top-country').load("/cmds/TopCountry.aspx");

    var searchCateId = $('#SearchCateCurrType').val();
    if (searchCateId == 2) {
        searchCategoryBtnGroup($('button[data-tag="systemComputer"]'));
    }
    else if (searchCateId == 3) {
        searchCategoryBtnGroup($('button[data-tag="notebook"]'));
    }
    else if (searchCateId == 1) {
        searchCategoryBtnGroup($('button[data-tag="part"]'));
    }
});

/* google stat */
try {
    _uacct = "UA-4447256-1";
    urchinTracker();
} catch (e) { }