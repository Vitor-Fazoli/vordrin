/**
 * Classe para gerenciar itens no inventário
 */
export class ItemManager {
    constructor(gridWidth, gridHeight) {
        this.gridWidth = gridWidth;
        this.gridHeight = gridHeight;
        this.grid = [];
        this.items = [];
        this.nextItemId = 1;

        this.initializeGrid();
    }

    /**
     * Inicializa a grade vazia
     */
    initializeGrid() {
        this.grid = Array(this.gridHeight).fill().map(() => Array(this.gridWidth).fill(null));
    }

    /**
     * Adiciona um novo item ao inventário
     * @param {string} name - Nome do item
     * @param {number} width - Largura do item em células
     * @param {number} height - Altura do item em células
     * @param {string} image - URL da imagem do item
     * @param {Object} position - Posição do item (opcional)
     * @returns {Object|null} - O item criado ou null se não foi possível adicionar
     */
    addItem(name, width, height, image, position = null) {
        // Cria um novo item
        const newItem = {
            id: this.nextItemId++,
            name,
            width,
            height,
            image
        };

        // Se não foi fornecida uma posição, encontre uma disponível
        if (!position) {
            position = this.findAvailablePosition(width, height);

            // Se não há espaço disponível, retorna null
            if (!position) {
                return null;
            }
        } else {
            // Verifica se a posição fornecida está disponível
            if (!this.isPositionAvailable(position.x, position.y, width, height)) {
                return null;
            }
        }

        // Atribui a posição ao item
        newItem.position = position;

        // Ocupa as células na grade
        this.occupyGridCells(newItem);

        // Adiciona o item à lista de itens
        this.items.push(newItem);

        return newItem;
    }

    /**
     * Remove um item do inventário
     * @param {number} itemId - ID do item a ser removido
     * @returns {boolean} - True se o item foi removido, False se não foi encontrado
     */
    removeItem(itemId) {
        const itemIndex = this.items.findIndex(item => item.id === itemId);

        if (itemIndex === -1) {
            return false;
        }

        const item = this.items[itemIndex];

        // Remove o item da grade
        this.clearGridCells(item);

        // Remove o item da lista
        this.items.splice(itemIndex, 1);

        return true;
    }

    /**
     * Move um item para uma nova posição
     * @param {number} itemId - ID do item a ser movido
     * @param {Object} newPosition - Nova posição {x, y}
     * @returns {boolean} - True se o item foi movido, False se não foi possível
     */
    moveItem(itemId, newPosition) {
        const item = this.items.find(item => item.id === itemId);

        if (!item) {
            return false;
        }

        // Verifica se a nova posição está dentro dos limites e disponível
        if (
            newPosition.x < 0 ||
            newPosition.y < 0 ||
            newPosition.x + item.width > this.gridWidth ||
            newPosition.y + item.height > this.gridHeight
        ) {
            return false;
        }

        // Remove o item da grade temporariamente
        this.clearGridCells(item);

        // Verifica se a nova posição está disponível
        if (!this.isPositionAvailable(newPosition.x, newPosition.y, item.width, item.height)) {
            // Se não estiver disponível, volta o item para a posição original
            this.occupyGridCells(item);
            return false;
        }

        // Atualiza a posição do item
        item.position = newPosition;

        // Ocupa as novas células
        this.occupyGridCells(item);

        return true;
    }

    /**
     * Troca dois itens de posição se possível
     * @param {number} itemId1 - ID do primeiro item
     * @param {number} itemId2 - ID do segundo item
     * @returns {boolean} - True se a troca foi feita, False se não foi possível
     */
    swapItems(itemId1, itemId2) {
        const item1 = this.items.find(item => item.id === itemId1);
        const item2 = this.items.find(item => item.id === itemId2);

        if (!item1 || !item2) {
            return false;
        }

        // Remove ambos os itens da grade temporariamente
        this.clearGridCells(item1);
        this.clearGridCells(item2);

        // Verifica se item1 cabe na posição de item2
        const canItem1FitInItem2Pos = this.isPositionAvailable(
            item2.position.x,
            item2.position.y,
            item1.width,
            item1.height
        );

        // Verifica se item2 cabe na posição de item1
        const canItem2FitInItem1Pos = this.isPositionAvailable(
            item1.position.x,
            item1.position.y,
            item2.width,
            item2.height
        );

        // Se ambos os itens não cabem nas posições trocadas, restaure e retorne false
        if (!canItem1FitInItem2Pos || !canItem2FitInItem1Pos) {
            this.occupyGridCells(item1);
            this.occupyGridCells(item2);
            return false;
        }

        // Troca as posições
        const tempPosition = { ...item1.position };
        item1.position = { ...item2.position };
        item2.position = tempPosition;

        // Ocupa as novas células
        this.occupyGridCells(item1);
        this.occupyGridCells(item2);

        return true;
    }

    /**
     * Encontra uma posição disponível para um item de tamanho específico
     * @param {number} width - Largura do item em células
     * @param {number} height - Altura do item em células
     * @returns {Object|null} - A posição {x, y} ou null se não houver espaço
     */
    findAvailablePosition(width, height) {
        for (let y = 0; y <= this.gridHeight - height; y++) {
            for (let x = 0; x <= this.gridWidth - width; x++) {
                if (this.isPositionAvailable(x, y, width, height)) {
                    return { x, y };
                }
            }
        }
        return null;
    }

    /**
     * Verifica se uma posição está disponível para um item de tamanho específico
     * @param {number} x - Coordenada X
     * @param {number} y - Coordenada Y
     * @param {number} width - Largura do item em células
     * @param {number} height - Altura do item em células
     * @returns {boolean} - True se a posição está disponível, False caso contrário
     */
    isPositionAvailable(x, y, width, height) {
        // Verifica se a posição está dentro dos limites da grade
        if (x < 0 || y < 0 || x + width > this.gridWidth || y + height > this.gridHeight) {
            return false;
        }

        // Verifica se todas as células necessárias estão disponíveis
        for (let j = y; j < y + height; j++) {
            for (let i = x; i < x + width; i++) {
                if (this.grid[j][i] !== null) {
                    return false;
                }
            }
        }

        return true;
    }

    /**
     * Ocupa as células da grade com um item
     * @param {Object} item - O item a ser colocado na grade
     */
    occupyGridCells(item) {
        const { x, y } = item.position;

        for (let j = y; j < y + item.height; j++) {
            for (let i = x; i < x + item.width; i++) {
                this.grid[j][i] = item.id;
            }
        }
    }

    /**
     * Limpa as células ocupadas por um item
     * @param {Object} item - O item a ser removido da grade
     */
    clearGridCells(item) {
        const { x, y } = item.position;

        for (let j = y; j < y + item.height; j++) {
            for (let i = x; i < x + item.width; i++) {
                if (i >= 0 && j >= 0 && i < this.gridWidth && j < this.gridHeight) {
                    this.grid[j][i] = null;
                }
            }
        }
    }

    /**
     * Obtém todos os itens do inventário
     * @returns {Array} - Lista de itens
     */
    getAllItems() {
        return [...this.items];
    }

    /**
     * Verifica se o inventário está cheio
     * @returns {boolean} - True se estiver cheio, False caso contrário
     */
    isFull() {
        for (let y = 0; y < this.gridHeight; y++) {
            for (let x = 0; x < this.gridWidth; x++) {
                if (this.grid[y][x] === null) {
                    return false;
                }
            }
        }
        return true;
    }

    /**
     * Calcula o espaço disponível no inventário
     * @returns {number} - Número de células livres
     */
    getAvailableSpace() {
        let freeCells = 0;

        for (let y = 0; y < this.gridHeight; y++) {
            for (let x = 0; x < this.gridWidth; x++) {
                if (this.grid[y][x] === null) {
                    freeCells++;
                }
            }
        }

        return freeCells;
    }

    /**
     * Organiza automaticamente o inventário para otimizar o espaço
     * Utiliza um algoritmo simples de bin packing
     * @returns {boolean} - True se o inventário foi reorganizado, False caso contrário
     */
    autoOrganize() {
        // Salva o estado atual do inventário
        const originalItems = JSON.parse(JSON.stringify(this.items));
        const originalGrid = JSON.parse(JSON.stringify(this.grid));

        // Limpa a grade
        this.initializeGrid();

        // Ordena os itens por tamanho (do maior para o menor)
        const sortedItems = [...this.items].sort((a, b) => {
            const areaA = a.width * a.height;
            const areaB = b.width * b.height;
            return areaB - areaA;
        });

        // Tenta colocar cada item na melhor posição possível
        let allItemsPlaced = true;

        for (const item of sortedItems) {
            const position = this.findAvailablePosition(item.width, item.height);

            if (position) {
                item.position = position;
                this.occupyGridCells(item);
            } else {
                // Se não conseguir posicionar algum item, restaura o estado original
                allItemsPlaced = false;
                break;
            }
        }

        // Se algum item não pôde ser posicionado, restaure o estado original
        if (!allItemsPlaced) {
            this.items = originalItems;
            this.grid = originalGrid;
            return false;
        }

        return true;
    }
}