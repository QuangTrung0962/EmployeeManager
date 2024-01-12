function ViewModel() {
    let self = this;
    self.selectedProvince = ko.observable();
    self.selectedDistrict = ko.observable();

    self.districts = ko.observableArray();
    self.towns = ko.observableArray();
    self.error = ko.observable();

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    self.selectedProvince.subscribe(function (provinceId) {
        let getDistrictsUri = '/api/getapi/GetDistrictsByProvinceId/' + provinceId;
        ajaxHelper(getDistrictsUri, 'GET').done(function (data) {
            self.districts(data);
        });
    });
    self.selectedDistrict.subscribe(function (districtId) {
        let getTownsUri = '/api/getapi/GetTownsByDistrictId/' + districtId;
        ajaxHelper(getTownsUri, 'GET').done(function (data) {
            self.towns(data);
        });
    });
}

// Kích hoạt KnockoutJS binding
let viewModel = new ViewModel();
ko.applyBindings(viewModel);