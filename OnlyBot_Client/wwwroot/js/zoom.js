(() => {
    let zoomLevel = 0.6048; // Niveau de zoom initial
    const minZoom = 0.5;   // Zoom minimum
    const maxZoom = 3;     // Zoom maximum
    let isDragging = false;
    let startX, startY;
    let offsetX = -2714.78; // Décalage initial en X
    let offsetY = -1878.56; // Décalage initial en Y

    const imageWidth = 250; // Largeur de chaque image
    const imageHeight = 250; // Hauteur de chaque image
    const mapWidth = 16 * imageWidth; // Largeur totale de la carte
    const mapHeight = 14 * imageHeight; // Hauteur totale de la carte
    const cellWidth = mapWidth / 161; // Largeur de chaque cellule de la grille
    const cellHeight = mapHeight / 144; // Hauteur de chaque cellule de la grille

    // Calculer les coordonnées de départ pour les IDs
    const startXCoord = -94;
    const startYCoord = -100;
    const xIncrement = (mapWidth / 13) / 144; // Largeur d'une cellule en X
    const yIncrement = (mapHeight / 16) / 161; // Hauteur d'une cellule en Y

    function createGrid() {
        const grille = document.getElementById('grille');
        if (grille) {
            for (let y = 0; y < 144; y++) {
                for (let x = 0; x < 161; x++) {
                    const cellule = document.createElement('div');
                    cellule.className = 'celluleGrille';
                    cellule.style.width = `${cellWidth}px`;
                    cellule.style.height = `${cellHeight}px`;
                    cellule.style.left = `${x * cellWidth}px`;
                    cellule.style.top = `${y * cellHeight}px`;

                    // Calculer les coordonnées ID
                    const coordX = Math.round(startXCoord + x * xIncrement);
                    const coordY = Math.round(startYCoord + y * yIncrement);
                    cellule.id = `${coordX}_${coordY}`; // Définir l'ID unique pour chaque cellule

                    grille.appendChild(cellule);
                }
            }
        }
    }

    function updateZoom() {
        const carte = document.getElementById('carte');
        const grille = document.getElementById('grille');
        if (carte && grille) {
            const transform = `scale(${zoomLevel}) translate(${offsetX}px, ${offsetY}px)`;
            carte.style.transform = transform;
            grille.style.transform = transform;
            carte.style.transformOrigin = 'top left';
            grille.style.transformOrigin = 'top left';
        }
    }

    function zoomIn(event) {
        const prevZoomLevel = zoomLevel;
        zoomLevel = Math.min(zoomLevel * 1.2, maxZoom); // Appliquer le zoom et respecter le maximum
        adjustOffset(event, prevZoomLevel, zoomLevel);
        updateZoom();
    }

    function zoomOut(event) {
        const prevZoomLevel = zoomLevel;
        zoomLevel = Math.max(zoomLevel / 1.2, minZoom); // Appliquer le zoom et respecter le minimum
        adjustOffset(event, prevZoomLevel, zoomLevel);
        updateZoom();
    }

    function adjustOffset(event, prevZoomLevel, newZoomLevel) {
        const rect = event.target.getBoundingClientRect();
        const cursorX = event.clientX - rect.left; // Position du curseur relative à la carte
        const cursorY = event.clientY - rect.top; // Position du curseur relative à la carte

        // Ajuster les décalages pour centrer le zoom sur le curseur
        offsetX -= cursorX * (newZoomLevel - prevZoomLevel) / prevZoomLevel;
        offsetY -= cursorY * (newZoomLevel - prevZoomLevel) / prevZoomLevel;
    }

    function initializeMap() {
        const carte = document.getElementById('carte');
        if (carte) {
            // Appliquer les valeurs initiales au style de la div
            carte.style.transform = `scale(${zoomLevel}) translate(${offsetX}px, ${offsetY}px)`;
            carte.style.transformOrigin = 'top left';
            createGrid();

            carte.addEventListener('wheel', (event) => {
                event.preventDefault();
                if (event.deltaY < 0) {
                    zoomIn(event);
                } else {
                    zoomOut(event);
                }
            });

            carte.addEventListener('mousedown', (event) => {
                isDragging = true;
                startX = event.clientX - offsetX;
                startY = event.clientY - offsetY;
            });

            carte.addEventListener('mousemove', (event) => {
                if (isDragging) {
                    const x = event.clientX;
                    const y = event.clientY;
                    offsetX = x - startX;
                    offsetY = y - startY;
                    updateZoom();
                }
            });

            carte.addEventListener('mouseup', () => {
                isDragging = false;
            });

            carte.addEventListener('mouseleave', () => {
                isDragging = false;
            });

            carte.addEventListener('touchstart', (event) => {
                isDragging = true;
                startX = event.touches[0].clientX - offsetX;
                startY = event.touches[0].clientY - offsetY;
            });

            carte.addEventListener('touchmove', (event) => {
                if (isDragging) {
                    const x = event.touches[0].clientX;
                    const y = event.touches[0].clientY;
                    offsetX = x - startX;
                    offsetY = y - startY;
                    updateZoom();
                }
            });

            carte.addEventListener('touchend', () => {
                isDragging = false;
            });
        }
    }

    document.addEventListener('DOMContentLoaded', () => {
        initializeMap();
    });

    // Expose the functions to the global scope for Blazor
    window.zoomIn = zoomIn;
    window.zoomOut = zoomOut;
    window.initializeMap = initializeMap;
})();
