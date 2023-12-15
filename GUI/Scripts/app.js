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
        let getDistrictsUri = '/api/testapi/GetDistrictsByProvinceId/' + provinceId;
        ajaxHelper(getDistrictsUri, 'GET').done(function (data) {
            self.districts(data);    
        });
    });
    self.selectedDistrict.subscribe(function (districtId) {
        // Đoạn code xử lý khi giá trị được chọn thay đổi District Id
        let getTownsUri = '/api/testapi/GetTownsByDistrictId/' + districtId;
        ajaxHelper(getTownsUri, 'GET').done(function (data) {
            self.towns(data);
        });
    });
}

// Kích hoạt KnockoutJS binding
let viewModel = new ViewModel();
ko.applyBindings(viewModel);