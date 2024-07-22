window.initializeMap = function (mapId, tilePath, tileSize, numCols, numRows) {
    var map = L.map(mapId, {
        crs: L.CRS.Simple,
        minZoom: 0,
        maxZoom: 4, // Définissez un niveau de zoom maximum raisonnable
        zoom: 0,
        center: [0, 0]
    });

    var bounds = [[0, 0], [numRows * tileSize, numCols * tileSize]];

    var layer = L.tileLayer(tilePath, {
        bounds: bounds,
        noWrap: true,
        tileSize: tileSize,
        maxZoom: 4, // Correspond au niveau de zoom maximum de la carte
        minZoom: 0,
        tms: false // Assure que les indices ne sont pas inversés
    }).addTo(map);

    map.fitBounds(bounds);

    function addGrid(map, bounds, tileSize) {
        var lines = [];
        for (var x = 0; x <= bounds[1][1]; x += tileSize) {
            lines.push([[bounds[0][1], x], [bounds[1][0], x]]);
        }
        for (var y = 0; y <= bounds[1][0]; y += tileSize) {
            lines.push([[y, bounds[0][0]], [y, bounds[1][1]]]);
        }
        lines.forEach(function (line) {
            L.polyline(line, { color: 'black', weight: 1, opacity: 0.5 }).addTo(map);
        });
    }

    addGrid(map, bounds, tileSize);

    // Fonction pour appliquer une transformation CSS en fonction du niveau de zoom
    map.on('zoom', function () {
        var zoomLevel = map.getZoom();
        var scale = Math.pow(2, zoomLevel); // Zoom exponentiel
        layer.getContainer().style.transform = 'scale(' + scale + ')';
        layer.getContainer().style.transformOrigin = 'top left';
    });

    return map;
};
