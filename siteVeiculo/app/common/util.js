

var PaginationModel = function (pagingData) {

    if (pagingData) {
        Object.assign(this, pagingData);
       
    }
    else {
        this.pageNumber = 1;
        this.orderBy = 'id';
        this.ascending = true;
        this.pageSize = 10;
        this.results = [];
    }

   /* {
        "pageNumber": 3,
        "orderBy": "id",
        "ascending": true,
        "pageSize": 2,
        "totalNumberOfPages": 3,
        "totalNumberOfRecords": 12,
        "nextPageUrl": null,
        "results": [
            {
                "Multa": [],
                "id": 11,
                "cor": "Vermelho",
                "modelo": "Uno",
                "placa": "ABC1234"
            },
            {
                "Multa": [],
                "id": 12,
                "cor": "Vermelho",
                "modelo": "Uno",
                "placa": "ABC1234"
            }
        ]
    }*/
           

    this.pageChanged = function () {
        console.log('Page changed to: ' + $scope.currentPage);
    };
    
}