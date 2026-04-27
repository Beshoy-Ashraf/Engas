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
                    sold: product.sold,
                    price: product.price
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
    card.onclick = () => openProductDetailModal(product);

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
    `;

    return card;
}

// Open product detail modal
window.openProductDetailModal = function(product) {
    const modal = document.getElementById('productDetailModal');
    const modalBody = document.getElementById('productDetailBody');

    if (!modal || !modalBody) return;

    const iconClass = categoryIcons[product.category] || 'fas fa-box';

    // Generate branches table
    const branchesTable = product.branches.map(branch => `
        <tr>
            <td>${branch.branchName}</td>
            <td>${branch.branchCode}</td>
            <td class="text-center">${branch.stock}</td>
            <td class="text-center">${branch.sold}</td>
            <td class="text-center">${branch.price ? formatCurrency(branch.price) : 'غير محدد'}</td>
        </tr>
    `).join('');

    modalBody.innerHTML = `
        <div class="product-detail-header">
            <div class="product-detail-icon">
                <i class="${iconClass}"></i>
            </div>
            <div class="product-detail-info">
                <h2>${product.name}</h2>
                <span class="product-detail-category">${product.category}</span>
            </div>
        </div>
        <div class="product-detail-stats">
            <div class="detail-stat">
                <i class="fas fa-box"></i>
                <span>إجمالي المخزون: ${product.totalStock}</span>
            </div>
            <div class="detail-stat">
                <i class="fas fa-check-circle"></i>
                <span>إجمالي المباع: ${product.totalSold}</span>
            </div>
            <div class="detail-stat">
                <i class="fas fa-building"></i>
                <span>عدد الفروع: ${product.branches.length}</span>
            </div>
        </div>
        <div class="product-branches-section">
            <h3>تفاصيل المنتج في الفروع</h3>
            <div class="branches-table-container">
                <table class="branches-table">
                    <thead>
                        <tr>
                            <th>اسم الفرع</th>
                            <th>كود الفرع</th>
                            <th>المخزون</th>
                            <th>المباع</th>
                            <th>السعر</th>
                        </tr>
                    </thead>
                    <tbody>
                        ${branchesTable}
                    </tbody>
                </table>
            </div>
        </div>
    `;

    modal.classList.add('active');
};

// Close product detail modal
window.closeProductDetailModal = function() {
    const modal = document.getElementById('productDetailModal');
    if (modal) {
        modal.classList.remove('active');
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

// Format currency helper
function formatCurrency(amount) {
    return new Intl.NumberFormat('ar-EG', {
        style: 'currency',
        currency: 'EGP',
        minimumFractionDigits: 0
    }).format(amount);
}

// Initialize on page load
document.addEventListener('DOMContentLoaded', () => {
    // Load branches data if available
    if (window.branches) {
        branches = window.branches;
    }
    renderProducts();
    updateStats();
});
