app.controller("VeiculoController", function ($rootScope, $scope, $state, $location, $window, $timeout, dashboardService, Flash, veiculoRepository) {
    console.log("coming to Veiculo controller");

    $window.document.title = "Veiculos";

    $scope.veiculoDefault = { id:0, cor: "", modelo: "", placa: "" };

    $scope.veiculo = $scope.veiculoDefault;

    $scope.paginationModel = new PaginationModel();    

    

    function listarVeiculos() {
        //console.log(veiculoRepository.getVeiculos());
        veiculoRepository.getVeiculos().then(success,fail);

        function success(response) {
            $scope.veiculos = response.data;
            console.log(response);

        }

        function fail(error) {
            alert("Erro ao obter dados");

        }

    }


    $scope.listarVeiculosTable =  function(pageNumber, pageSize) {
        //console.log(veiculoRepository.getVeiculos());
        if (typeof pageNumber === "undefined" || pageNumber === null) {
            pageNumber = 1;
        }

        if (typeof pageSize === "undefined" || pageSize === null) {
            pageSize = 5;
        }

        veiculoRepository.getVeiculosTable({ pageNumber: pageNumber, orderBy: "id", ascending: true, pageSize: pageSize}).then(success, fail);

        function success(response) {
            $scope.paginationModel = new PaginationModel(response.data);
            $scope.veiculos = response.data.results;

            //$timeout(function () {
            //   // $scope.$apply();
            //}, 500);
            
            console.log(response);

        }

        function fail(error) {
            alert("Erro ao obter dados");

        }

    }



    $scope.salvarVeiculo = function () {
        var modelVeiculo = $scope.veiculo;

        if (modelVeiculo.id === 0) {
            var a = veiculoRepository.postVeiculo(modelVeiculo).then(success, fail);
        } else {
            veiculoRepository.putVeiculo(modelVeiculo).then(success, fail);
        }




        function success(response) {
            $scope.veiculos = response.data;
            console.log(response);

        }

        function fail(error) {
            alert("Erro ao inserir dados");

        }
    };


    $scope.excluirVeiculo = function (veiculo) {
        
       
        var q = veiculoRepository.deleteVeiculo(veiculo.id).then(success, fail);


        function success(response) {

            $scope.paginationModel.results
            
            var index = $scope.paginationModel.results.indexOf(veiculo);
            $scope.paginationModel.results.splice(index, 1);
            alert("Item deletado com sucesso!");

            console.log(response);

        }

        function fail(error) {
            alert("Erro ao deletar dados");

        }
    };

    $scope.abrirEdicaoVeiculo = function (veiculo) {
        $scope.veiculo = veiculo;


    };

    

    




    $scope.listarVeiculosTable();

});