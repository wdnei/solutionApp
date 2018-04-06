app.factory("veiculoRepository", function ($resource, $http) {
    var baseUrl = "http://localhost:50193";
    var headers = { "Content-Type": "application/json;charset=utf-8" };

    return {
        getVeiculos: function () {
            return $http({
                method: "get",
                url: baseUrl + "/api/veiculo/getVeiculos",
                headers: headers
            });
        },
        getVeiculosTable: function (pagingModel) {
            return $http({
                method: "get",
                url: baseUrl + "/api/veiculo/getVeiculosTable",
                headers: headers,
                params: pagingModel
            });
        },
        postVeiculo: function (modelVeiculo) {
            return $http({
                method: "post",
                url: baseUrl + "/api/veiculo/addVeiculo",
                headers: headers,
                data: JSON.stringify(modelVeiculo)
            });
        },
        putVeiculo: function (modelVeiculo) {
            return $http({
                method: "put",
                url: baseUrl + "/api/veiculo/updateVeiculo",
                headers: headers,
                data: JSON.stringify(modelVeiculo)
            });
        },
        deleteVeiculo: function (idVeiculo) {
            return $http({
                method: "delete",
                url: baseUrl + "/api/veiculo/deleteVeiculo",
                headers: headers,
                params: { id: idVeiculo }
            });
        }
    };

});

