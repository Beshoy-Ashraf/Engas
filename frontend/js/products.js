// Product categories icons mapping
const categoryIcons = {
    'هواتف': 'fas fa-mobile-alt',
    'أجهزة لابتوب': 'fas fa-laptop',
    'أجهزة لوحية': 'fas fa-tablet-alt',
    'تليفزيونات': 'fas fa-tv',
    'سماعات': 'fas fa-headphones',
    'إكسسوارات': 'fas fa-plug',
    'صوت': 'fas fa-speaker',
    'أجهزة منزلية': 'fas fa-blender'
};

// Get all unique products from all branches with aggregated stock
function getAllProducts() {
    const allProducts = new Map();
    
    if (typeof branches === 'undefined') {
        return [];
    }
    
    branches.forEach(branch => {
        if (branch.products && Array.isArray(branch.products)) {
            branch.products.forEach(product => {
                if (!allProducts.has(product.name)) {
                    allProducts.set(product.name, {
                        name: product.name,
                        category: product.category,
                        branches: [],
                        totalStock: 0,
                        totalSold: 0
                    });
                }
                const p = allProducts.get(product.name);
                p.branches.push({
                    branchId: branch.id,
                    branchName: branch.name,
                    branchCode: branch.code,
                    stock: product.stock,
                    sold: product.sold
                });
                p.totalStock += product.stock;
                p.totalSold += product.sold;
            });
        }
    });
    return Array.from(allProducts.values());
}

let currentCategory = 'all';

// Render products grid
function renderProducts(category = 'all') {
    const grid = document.getElementById('productsGrid');
    if (!grid) return;

    const allProducts = getAllProducts();
    const filtered = category === 'all' ? allProducts : allProducts.filter(p => p.category === category);

    if (filtered.length === 0) {
        grid.innerHTML = `
            <div class="no-products" style="grid-column: 1/-1; text-align: center; padding: 60px 20px; color: var(--text-muted);">
                <i class="fas fa-box-open" style="font-size: 64px; margin-bottom: 20px; opacity: 0.3;"></i>
                <h3>لا توجد منتجات</h3>
                <p>لم يتم العثور على منتجات في هذا التصنيف</p>
            </div>
        `;
        return;
    }

    grid.innerHTML = '';
    filtered.forEach((product, index) => {
        const card = createProductCard(product);
        card.style.animationDelay = `${index * 0.05}s`;
        grid.appendChild(card);
    });
}

// Create product card
function createProductCard(product) {
    const card = document.createElement('div');
    card.className = 'product-card';
    card.style.animation = 'card-appear 0.4s ease forwards';

    const iconClass = categoryIcons[product.category] || 'fas fa-box';
    const stockPercentage = Math.min((product.totalStock / 100) * 100, 100);

    card.innerHTML = `
        <div class="product-icon">
            <i class="${iconClass}"></i>
        </div>
        <div class="product-info">
            <div class="product-name">${product.name}</div>
            <div class="product-category">${product.category}</div>
            <div class="product-stats">
                <div class="stat">
                    <i class="fas fa-box"></i>
                    <span>${product.totalStock}</span>
                </div>
                <div class="stat sold">
                    <i class="fas fa-check-circle"></i>
                    <span>${product.totalSold}</span>
                </div>
            </div>
            <div class="stock-bar-container">
                <div class="stock-bar" style="width: ${stockPercentage}%"></div>
            </div>
            <span class="stock-text">المخزون: ${product.totalStock} | المباع: ${product.totalSold}</span>
        </div>
        <div class="product-branches-toggle" onclick="toggleBranchesList(this)">
            <div class="branches-summary">
                <i class="fas fa-building toggle-icon"></i>
                <span>الفرع${product.branches.length > 1 ? 'وفروع' : ''}</span>
                <i class="fas fa-chevron-down toggle-icon"></i>
            </div>
            <div class="branches-list" style="display: none;">
                ${product.branches.map(branch => `
                    <div class="branch-item">
                        <div class="branch-header">
                            <span class="branch-name">${branch.branchName}</span>
                            <span class="branch-code">[${branch.branchCode}]</span>
                        </div>
                        <div class="branch-stats">
                            <span class="branch-stat stock"><i class="fas fa-archive"></i> المخزون: ${branch.stock}</span>
                            <span class="branch-stat sold"><i class="fas fa-check"></i> المباع: ${branch.sold}</span>
                        </div>
                    </div>
                `).join('')}
            </div>
        </div>
    `;

    return card;
}

// Toggle branches list visibility
window.toggleBranchesList = function(toggleElement) {
    const branchesList = toggleElement.querySelector('.branches-list');
    const icon = toggleElement.querySelector('.toggle-icon:last-child');
    
    if (branchesList.style.display === 'none' || branchesList.style.display === '') {
        branchesList.style.display = 'block';
        icon.classList.remove('fa-chevron-down');
        icon.classList.add('fa-chevron-up');
    } else {
        branchesList.style.display = 'none';
        icon.classList.remove('fa-chevron-up');
        icon.classList.add('fa-chevron-down');
    }
};

// Filter by category
window.filterByCategory = function(category) {
    currentCategory = category;
    
    // Update active button
    document.querySelectorAll('.category-btn').forEach(btn => {
        btn.classList.remove('active');
        if (btn.dataset.category === category) {
            btn.classList.add('active');
        }
    });
    
    renderProducts(category);
};

// Update statistics
function updateStats() {
    const allProducts = getAllProducts();
    const branchesCount = typeof branches !== 'undefined' ? branches.length : 0;
    
    const totalProductsEl = document.getElementById('totalProducts');
    const totalBranchesEl = document.getElementById('totalBranches');
    
    if (totalProductsEl) totalProductsEl.textContent = allProducts.length;
    if (totalBranchesEl) totalBranchesEl.textContent = branchesCount;
}

// Toast notification
window.showToast = function(message) {
    const toast = document.getElementById('successToast');
    const toastMessage = document.getElementById('toastMessage');
    if (toast && toastMessage) {
        toastMessage.textContent = message;
        toast.classList.add('show');
        setTimeout(() => hideToast(), 3000);
    }
};

window.hideToast = function() {
    const toast = document.getElementById('successToast');
    if (toast) {
        toast.classList.remove('show');
    }
};

// Initialize on page load
document.addEventListener('DOMContentLoaded', () => {
    // Load branches data if available
    if (window.branches) {
        branches = window.branches;
    }
    renderProducts();
    updateStats();
});
