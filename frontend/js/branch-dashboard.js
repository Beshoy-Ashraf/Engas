// Branch Dashboard JavaScript

// Sample products data
let products = [
    { id: 1, name: 'هاتف iPhone 15 Pro Max', model: '512GB - فضي', quantity: 25, price: 4500.00 },
    { id: 2, name: 'هاتف Samsung Galaxy S24 Ultra', model: '512GB - أسود', quantity: 18, price: 4200.00 },
    { id: 3, name: 'شاشة عرض LED 55 بوصة', model: 'UHD 4K - Smart TV', quantity: 12, price: 2800.00 },
    { id: 4, name: 'سماعات AirPods Pro', model: 'جيل 2 - سلكي', quantity: 45, price: 950.00 },
    { id: 5, name: 'شاحن سريع Type-C 65W', model: 'PD 3.0 - أسود', quantity: 120, price: 120.00 },
    { id: 6, name: 'حافظة حماية iPhone 15', model: 'سيليكون شفاف', quantity: 200, price: 80.00 },
    { id: 7, name: 'شاشة Smart TV 65 بوصة', model: 'OLED - 4K - أندرويد', quantity: 8, price: 4500.00 },
    { id: 8, name: 'كابل شحن مضاعف Type-C', model: '1M - 100W', quantity: 300, price: 45.00 }
];

// Sample orders data
let orders = [
    { id: 'ORD-001', date: '2026-04-24', products: [{ name: 'هاتف iPhone 15 Pro Max', model: '512GB - فضي', quantity: 2 }], total: 9000.0, status: 'completed', payment: 'فاليو' },
    { id: 'ORD-002', date: '2026-04-24', products: [{ name: 'سماعات AirPods Pro', model: 'جيل 2 - سلكي', quantity: 2 }, { name: 'شاحن سريع Type-C 65W', model: 'PD 3.0 - أسود', quantity: 1 }], total: 2020.0, status: 'pending', payment: 'فرصة' },
    { id: 'ORD-003', date: '2026-04-23', products: [{ name: 'شاشة عرض LED 55 بوصة', model: 'UHD 4K - Smart TV', quantity: 1 }], total: 2800.0, status: 'completed', payment: 'حالا' },
    { id: 'ORD-004', date: '2026-04-23', products: [{ name: 'شاشة Smart TV 65 بوصة', model: 'OLED - 4K - أندرويد', quantity: 1 }], total: 4500.0, status: 'cancelled', payment: 'بنك CIB' },
    { id: 'ORD-005', date: '2026-04-22', products: [{ name: 'هاتف Samsung Galaxy S24 Ultra', model: '512GB - أسود', quantity: 1 }, { name: 'حافظة حماية iPhone 15', model: 'سيليكون شفاف', quantity: 3 }], total: 4440.0, status: 'completed', payment: 'سهولة' }
];

// Initialize dashboard
let nextProductId = 9;

function formatCurrency(amount) {
    return new Intl.NumberFormat('ar-SA', {
        style: 'currency',
        currency: 'EGP '
    }).format(amount);
}

function updateStats() {
    const totalProducts = products.length;
    const totalValue = products.reduce((sum, p) => sum + (p.quantity * p.price), 0);
    const todayOrders = orders.filter(o => o.date === '2026-04-24').length;
    const monthOrders = orders.length;

    document.getElementById('totalProducts').textContent = totalProducts;
    document.getElementById('totalProductsValue').textContent = formatCurrency(totalValue);
    document.getElementById('todayOrders').textContent = todayOrders;
    document.getElementById('monthOrders').textContent = monthOrders;
}

function renderProducts() {
    const grid = document.getElementById('productsGrid');
    
    if (products.length === 0) {
        grid.innerHTML = `
            <div class="empty-state">
                <i class="fas fa-box-open"></i>
                <h3>لا توجد أصناف بعد</h3>
                <p>اضغط على زر "إضافة منتج" لإضافة أول صنف</p>
            </div>
        `;
        return;
    }

    grid.innerHTML = products.map(p => `
        <div class="product-card" data-id="${p.id}">
            <div class="product-card-header">
                <span class="product-id">ID: ${p.id.toString().padStart(4, '0')}</span>
                <div class="product-actions">
                    <button class="product-action-btn edit" onclick="editProduct(${p.id})" title="تعديل">
                        <i class="fas fa-edit"></i>
                    </button>
                    <button class="product-action-btn delete" onclick="deleteProduct(${p.id})" title="حذف">
                        <i class="fas fa-trash"></i>
                    </button>
                </div>
            </div>
            <div class="product-name">${p.name}</div>
            <div class="product-model">${p.model}</div>
            <div class="product-stats">
                <div class="product-stat">
                    <div class="product-stat-label">الكمية المتاحة</div>
                    <div class="product-stat-value">${p.quantity}</div>
                </div>
                <div class="product-stat">
                    <div class="product-stat-label">السعر للوحدة</div>
                    <div class="product-stat-value price">${formatCurrency(p.price)}</div>
                </div>
            </div>
        </div>
    `).join('');
}

function renderOrders() {
    const list = document.getElementById('ordersList');
    
    if (orders.length === 0) {
        list.innerHTML = `
            <div class="empty-state">
                <i class="fas fa-receipt"></i>
                <h3>لا توجد طلبات</h3>
                <p>لا توجد طلبات لعرضها حالياً</p>
            </div>
        `;
        return;
    }

    list.innerHTML = orders.slice(0, 5).map(o => `
        <div class="order-card">
            <div class="order-header">
                <span class="order-id">${o.id}</span>
                <span class="order-status ${o.status}">
                    ${getStatusText(o.status)}
                </span>
            </div>
            <div class="order-products">
                ${o.products.map(p => `
                    <div class="order-product">
                        <span class="order-product-info">
                            ${p.name} - ${p.model}
                        </span>
                        <span class="order-product-quantity">
                            ${p.quantity} × ${formatCurrency(products.find(pr => pr.name === p.name)?.price || 0)}
                        </span>
                    </div>
                `).join('')}
            </div>
            <div class="order-footer">
                <span class="order-total">${formatCurrency(o.total)}</span>
                <span class="order-payment">
                    <i class="fas fa-credit-card"></i>
                    ${o.payment}
                </span>
            </div>
        </div>
    `).join('');
}

function getStatusText(status) {
    const statusMap = {
        'completed': 'مكتمل',
        'pending': 'قيد الانتظار',
        'cancelled': 'ملغي'
    };
    return statusMap[status] || status;
}

// Modal functions
function openModal() {
    document.getElementById('productModal').classList.add('active');
    document.getElementById('modalTitle').textContent = 'إضافة منتج جديد';
    document.getElementById('productForm').reset();
    document.getElementById('productId').value = '';
    
    // Reset placeholder text for add mode
    document.getElementById('productName').placeholder = 'مثال: هاتف iPhone 15 Pro Max';
    document.getElementById('productModel').placeholder = 'مثال: 512GB - أسود';
}

function closeModal() {
    document.getElementById('productModal').classList.remove('active');
}

function editProduct(id) {
    const product = products.find(p => p.id === id);
    if (!product) return;

    document.getElementById('modalTitle').textContent = 'تعديل المنتج';
    document.getElementById('productId').value = product.id;
    document.getElementById('productName').value = product.name;
    document.getElementById('productModel').value = product.model;
    document.getElementById('productQuantity').value = product.quantity;
    document.getElementById('productPrice').value = product.price;
    
    // Set placeholder text for edit mode
    document.getElementById('productName').placeholder = 'اسم المنتج';
    document.getElementById('productModel').placeholder = 'موديل المنتج';
    
    // Open modal directly without calling openModal() to avoid resetting to add mode
    document.getElementById('productModal').classList.add('active');
}

function deleteProduct(id) {
    if (confirm('هل أنت متأكد من حذف هذا المنتج؟')) {
        products = products.filter(p => p.id !== id);
        renderProducts();
        updateStats();
        showNotification('تم حذف المنتج بنجاح');
    }
}

function saveProduct(e) {
    e.preventDefault();
    
    const id = document.getElementById('productId').value;
    const productData = {
        name: document.getElementById('productName').value,
        model: document.getElementById('productModel').value,
        quantity: parseInt(document.getElementById('productQuantity').value) || 0,
        price: parseFloat(document.getElementById('productPrice').value) || 0
    };

    if (id) {
        // Edit existing product
        const index = products.findIndex(p => p.id === parseInt(id));
        if (index !== -1) {
            products[index] = { ...products[index], ...productData };
            showNotification('تم تعديل المنتج بنجاح');
        }
    } else {
        // Add new product
        products.push({
            id: nextProductId++,
            ...productData
        });
        showNotification('تم إضافة المنتج بنجاح');
    }

    closeModal();
    renderProducts();
    updateStats();
}

function showNotification(message) {
    const notification = document.createElement('div');
    notification.style.cssText = `
        position: fixed;
        top: 20px;
        left: 50%;
        transform: translateX(-50%);
        background: var(--primary);
        color: var(--bg-primary);
        padding: 12px 24px;
        border-radius: 10px;
        z-index: 10001;
        font-weight: 500;
        animation: slideDown 0.3s ease;
    `;
    notification.innerHTML = `
        <style>
            @keyframes slideDown {
                from { opacity: 0; transform: translateX(-50%) translateY(-20px); }
                to { opacity: 1; transform: translateX(-50%) translateY(0); }
            }
        </style>
        <i class="fas fa-check-circle" style="margin-left: 8px;"></i>
        ${message}
    `;
    document.body.appendChild(notification);
    
    setTimeout(() => {
        notification.style.transition = 'opacity 0.3s ease';
        notification.style.opacity = '0';
        setTimeout(() => notification.remove(), 300);
    }, 3000);
}

function filterProducts() {
    const searchTerm = (document.getElementById('searchInput')?.value || '').toLowerCase();
    const filterValue = document.getElementById('filterSelect')?.value || 'all';
    const productCards = document.querySelectorAll('.product-card');
    
    productCards.forEach(card => {
        const name = card.querySelector('.product-name')?.textContent.toLowerCase() || '';
        const model = card.querySelector('.product-model')?.textContent.toLowerCase() || '';
        const matchesSearch = name.includes(searchTerm) || model.includes(searchTerm);
        
        let matchesFilter = true;
        if (filterValue !== 'all') {
            if (filterValue === 'phones' && !name.includes('هاتف') && !name.includes('iphone') && !name.includes('samsung')) {
                matchesFilter = false;
            } else if (filterValue === 'screens' && !name.includes('شاشة') && !name.includes('tv')) {
                matchesFilter = false;
            } else if (filterValue === 'accessories') {
                const isAccessory = name.includes('سماعة') || name.includes('حافظة') || name.includes('شاحن');
                matchesFilter = isAccessory;
            } else if (filterValue === 'chargers' && !name.includes('شاحن') && !name.includes('كابل')) {
                matchesFilter = false;
            }
        }
        
        if (matchesSearch && matchesFilter) {
            card.style.display = '';
        } else {
            card.style.display = 'none';
        }
    });
}

// Event listeners
document.addEventListener('DOMContentLoaded', function() {
    updateStats();
    renderProducts();
    renderOrders();

    // Add search and filter event listeners
    const searchInput = document.getElementById('searchInput');
    const filterSelect = document.getElementById('filterSelect');
    
    if (searchInput) {
        searchInput.addEventListener('input', filterProducts);
    }
    if (filterSelect) {
        filterSelect.addEventListener('change', filterProducts);
    }

    // Add product button
    document.getElementById('addProductBtn').addEventListener('click', openModal);

    // Close modal events
    document.getElementById('modalClose').addEventListener('click', closeModal);
    document.getElementById('cancelBtn').addEventListener('click', closeModal);
    document.getElementById('productModal').addEventListener('click', function(e) {
        if (e.target === this) closeModal();
    });

    // Save product form
    document.getElementById('productForm').addEventListener('submit', saveProduct);

    // Close modal on Escape key
    document.addEventListener('keydown', function(e) {
        if (e.key === 'Escape') closeModal();
    });
});