// Sample branches data (in real app, this would come from backend)
let branches = [
    {
        id: 1,
        name: "فرع المهندسين",
        code: "BR-001",
        phone: "02-1234-5678",
        manager: "أحمد محمود",
        governorate: "القاهرة",
        address: "المهندسين - شارع التحرير",
        status: "active",
        createdAt: "2025-06-01",
        salesCount: 156,
        totalSales: 1250000,
        products: [
            { name: "iPhone 15 Pro", category: "هواتف", stock: 25, sold: 45 },
            { name: "Samsung Galaxy S24", category: "هواتف", stock: 30, sold: 32 },
            { name: "MacBook Pro 16\"", category: "أجهزة لابتوب", stock: 12, sold: 18 },
            { name: "Dell XPS 15", category: "أجهزة لابتوب", stock: 20, sold: 15 }
        ],
        recentSales: [
            { customer: "أحمد محمد علي", items: 3, total: 84600, time: "منذ 3 ساعات" },
            { customer: "سارة محمود", items: 1, total: 45000, time: "منذ 5 ساعات" }
        ]
    },
    {
        id: 2,
        name: "فرع 6 أكتوبر",
        code: "BR-002",
        phone: "02-9876-5432",
        manager: "محمد حسين",
        governorate: "الجيزة",
        address: "6 أكتوبر - المنطقة السادسة",
        status: "active",
        createdAt: "2025-07-15",
        salesCount: 98,
        totalSales: 780000,
        products: [
            { name: "iPad Air", category: "أجهزة لوحية", stock: 18, sold: 24 },
            { name: "Sony WH-1000XM5", category: "سماعات", stock: 22, sold: 38 },
            { name: "Apple Watch Ultra", category: "إكسسوارات", stock: 15, sold: 22 }
        ],
        recentSales: [
            { customer: "فاطمة حسن", items: 2, total: 24000, time: "منذ 5 ساعات" }
        ]
    },
    {
        id: 3,
        name: "فرع مصر الجديدة",
        code: "BR-003",
        phone: "02-5555-1234",
        manager: "خالد عبد الرحمن",
        governorate: "القاهرة",
        address: "مصر الجديدة - شارع العروبة",
        status: "active",
        createdAt: "2025-08-20",
        salesCount: 124,
        totalSales: 980000,
        products: [
            { name: "Samsung 65\" QLED", category: "تليفزيونات", stock: 8, sold: 12 },
            { name: "LG OLED 55\"", category: "تليفزيونات", stock: 6, sold: 9 },
            { name: "Home Theater System", category: "صوت", stock: 14, sold: 21 }
        ],
        recentSales: []
    },
    {
        id: 4,
        name: "فرع التجمع",
        code: "BR-004",
        phone: "02-7777-8888",
        manager: "عمر السيد",
        governorate: "القاهرة",
        address: "التجمع الخامس - شارع محمد حسين",
        status: "active",
        createdAt: "2025-09-10",
        salesCount: 201,
        totalSales: 1850000,
        products: [
            { name: "Gaming Laptop ASUS", category: "أجهزة لابتوب", stock: 10, sold: 28 },
            { name: "Mechanical Keyboard", category: "إكسسوارات", stock: 35, sold: 52 },
            { name: "Gaming Mouse", category: "إكسسوارات", stock: 40, sold: 65 }
        ],
        recentSales: [
            { customer: "سارة محمود", items: 3, total: 71500, time: "منذ 4 أيام" }
        ]
    },
    {
        id: 5,
        name: "فرع سموحة",
        code: "BR-005",
        phone: "03-1234567",
        manager: "يوسف أحمد",
        governorate: "الإسكندرية",
        address: "سموحة - شارع الملك فيصل",
        status: "inactive",
        createdAt: "2025-05-28",
        salesCount: 0,
        totalSales: 0,
        products: [],
        recentSales: []
    },
    {
        id: 6,
        name: "فرع الإسماعيلية",
        code: "BR-006",
        phone: "064-123456",
        manager: "حسن علي",
        governorate: "الإسماعيلية",
        address: "الإسماعيلية - حي 3",
        status: "active",
        createdAt: "2025-10-05",
        salesCount: 67,
        totalSales: 420000,
        products: [
            { name: "Xiaomi Redmi Note 13", category: "هواتف", stock: 50, sold: 42 },
            { name: "Realme GT Neo 3", category: "هواتف", stock: 35, sold: 28 },
            { name: "شاحن لاسلكي", category: "إكسسوارات", stock: 100, sold: 85 }
        ],
        recentSales: [
            { customer: "عمر يوسف", items: 2, total: 20000, time: "منذ 6 أيام" }
        ]
    },
    {
        id: 7,
        name: "فرع الدقي",
        code: "BR-007",
        phone: "02-3333-4444",
        manager: "سعيد محمد",
        governorate: "الجيزة",
        address: "الدقي - شارع الجامعة",
        status: "active",
        createdAt: "2025-11-12",
        salesCount: 89,
        totalSales: 670000,
        products: [
            { name: "iPad Pro 12.9\"", category: "أجهزة لوحية", stock: 7, sold: 14 },
            { name: "AirPods Max", category: "سماعات", stock: 10, sold: 8 },
            { name: "Mac Mini", category: "أجهزة لابتوب", stock: 6, sold: 9 }
        ],
        recentSales: []
    },
    {
        id: 8,
        name: "فرع المنصورة",
        code: "BR-008",
        phone: "050-1234567",
        manager: "إبراهيم علي",
        governorate: "الدقهلية",
        address: "المنصورة - شارع الجيش",
        status: "active",
        createdAt: "2025-12-01",
        salesCount: 45,
        totalSales: 310000,
        products: [
            { name: "Samsung A54", category: "هواتف", stock: 25, sold: 31 },
            { name: "Xiaomi Poco X6", category: "هواتف", stock: 30, sold: 25 }
        ],
        recentSales: []
    }
];

// Product categories mapping
const productCategories = {
    'هواتف': 'fas fa-mobile-alt',
    'أجهزة لابتوب': 'fas fa-laptop',
    'أجهزة لوحية': 'fas fa-tablet-alt',
    'تليفزيونات': 'fas fa-tv',
    'سماعات': 'fas fa-headphones',
    'إكسسوارات': 'fas fa-plug',
    'صوت': 'fas fa-speaker',
    'أجهزة منزلية': 'fas fa-blender'
};

// Initialize the page
document.addEventListener('DOMContentLoaded', () => {
    loadBranches();
    updateStats();
    initializeModals();
});

// Load branches into grid
function loadBranches(data = branches) {
    const grid = document.getElementById('branchesGrid');
    grid.innerHTML = '';

    if (data.length === 0) {
        document.getElementById('noResults').style.display = 'flex';
        return;
    }

    document.getElementById('noResults').style.display = 'none';

    data.forEach((branch, index) => {
        const card = createBranchCard(branch);
        card.style.animationDelay = `${index * 0.05}s`;
        grid.appendChild(card);
    });
}

// Create branch card element
function createBranchCard(branch) {
    const card = document.createElement('div');
    card.className = 'branch-card';
    card.onclick = () => openBranchDetailModal(branch.id);

    const statusClass = branch.status === 'active' ? 'status-active' : 'status-inactive';
    const statusIcon = branch.status === 'active' ? 'fa-check-circle' : 'fa-times-circle';
    const statusText = branch.status === 'active' ? 'نشط' : 'غير نشط';

    // Get first product category icon
    const firstProductIcon = branch.products.length > 0 
        ? (productCategories[branch.products[0].category] || 'fas fa-box')
        : 'fas fa-box';

    card.innerHTML = `
        <div class="branch-card-header">
            <div class="branch-avatar">
                <i class="fas ${firstProductIcon}"></i>
            </div>
            <div class="branch-info">
                <div class="branch-name">${branch.name}</div>
                <div class="branch-code">${branch.code}</div>
            <div class="branch-status ${statusClass}">
                <i class="fas ${statusIcon}"></i>
                ${statusText}
            </div>
        <div class="branch-details">
            <div class="detail-row">
                <i class="fas fa-phone"></i>
                <span>${branch.phone}</span>
            </div>
            <div class="detail-row">
                <i class="fas fa-map-marker-alt"></i>
                <span>${branch.governorate} - ${branch.address}</span>
            </div>
            <div class="detail-row">
                <i class="fas fa-user-tie"></i>
                <span>${branch.manager}</span>
            </div>
        <div class="branch-stats">
            <div class="stat-item">
                <i class="fas fa-shopping-cart"></i>
                <span class="stat-value">${branch.salesCount}</span>
                <span class="stat-label">مبيعات</span>
            </div>
            <div class="stat-item">
                <i class="fas fa-box"></i>
                <span class="stat-value">${branch.products.length}</span>
                <span class="stat-label">منتج</span>
            </div>
            <div class="stat-item">
                <i class="fas fa-dollar-sign"></i>
                <span class="stat-value">${formatCurrency(branch.totalSales)}</span>
                <span class="stat-label">إجمالي</span>
            </div>
        <div class="branch-actions">
            <button class="action-btn-icon edit-btn" onclick="event.stopPropagation(); openEditModal(${branch.id})" title="تعديل">
                <i class="fas fa-edit"></i>
            </button>
            <button class="action-btn-icon delete-btn" onclick="event.stopPropagation(); openDeleteModal(${branch.id})" title="حذف">
                <i class="fas fa-trash"></i>
            </button>
        </div>
    `;

    return card;
}

// Open branch detail modal
function openBranchDetailModal(branchId) {
    const branch = branches.find(b => b.id === branchId);
    if (!branch) return;

    const modalBody = document.getElementById('branchDetailBody');

    const statusClass = branch.status === 'active' ? 'status-active' : 'status-inactive';
    const statusText = branch.status === 'active' ? 'نشط' : 'غير نشط';

    // Get category stats
    const categoryStats = {};
    branch.products.forEach(p => {
        categoryStats[p.category] = (categoryStats[p.category] || 0) + p.sold;
    });

    modalBody.innerHTML = `
        <div class="branch-profile">
            <div class="branch-profile-icon">
                <i class="fas fa-code-branch"></i>
            </div>
            <div class="branch-profile-info">
                <div class="branch-profile-name">${branch.name}</div>
                <div class="branch-profile-code">${branch.code}</div>
                <div style="margin-top: 8px;">
                    <span class="branch-status ${statusClass}" style="font-size: 12px; padding: 4px 12px;">
                        <i class="fas ${statusIcon}"></i>
                        ${statusText}
                    </span>
                </div>
        </div>

        <div class="details-section">
            <h3 class="section-title"><i class="fas fa-info-circle"></i> معلومات الاتصال</h3>
            <div class="details-grid">
                <div class="detail-item">
                    <span class="detail-label">رقم الهاتف</span>
                    <span class="detail-value">${branch.phone}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">المحافظة</span>
                    <span class="detail-value">${branch.governorate}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">العنوان</span>
                    <span class="detail-value">${branch.address}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">مدير الفرع</span>
                    <span class="detail-value">${branch.manager}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">تاريخ التسجيل</span>
                    <span class="detail-value">${formatDate(branch.createdAt)}</span>
                </div>
        </div>

        <div class="stats-section">
            <h3 class="section-title"><i class="fas fa-chart-line"></i> إحصائيات المبيعات</h3>
            <div class="branch-stats-grid">
                <div class="branch-stat-card">
                    <div class="stat-icon">
                        <i class="fas fa-shopping-cart"></i>
                    </div>
                    <div class="stat-info">
                        <div class="stat-value">${branch.salesCount}</div>
                        <div class="stat-label">عدد المبيعات</div>
                </div>
                <div class="branch-stat-card">
                    <div class="stat-icon">
                        <i class="fas fa-box"></i>
                    </div>
                    <div class="stat-info">
                        <div class="stat-value">${branch.products.length}</div>
                        <div class="stat-label">المنتجات</div>
                </div>
                <div class="branch-stat-card highlight">
                    <div class="stat-icon">
                        <i class="fas fa-dollar-sign"></i>
                    </div>
                    <div class="stat-info">
                        <div class="stat-value">${formatCurrency(branch.totalSales)}</div>
                        <div class="stat-label">إجمالي المبيعات</div>
                </div>
        </div>

        <div class="actions-section" style="margin: 20px 0; display: flex; gap: 12px;">
            <button class="btn btn-next" onclick="openEditModal(${branch.id})">
                <i class="fas fa-edit"></i>
                <span>تعديل البيانات</span>
            </button>
            <button class="btn btn-prev" onclick="openDeleteModal(${branch.id})" style="border-color: #ff4757; color: #ff4757;">
                <i class="fas fa-trash"></i>
                <span>حذف الفرع</span>
            </button>
        </div>

        <div class="products-section">
            <h3 class="section-title"><i class="fas fa-box-open"></i> المنتجات المتاحة</h3>
            <div class="products-grid">
                ${branch.products.length > 0 ? branch.products.map(product => {
                    const iconClass = productCategories[product.category] || 'fas fa-box';
                    const stockPercentage = Math.min((product.stock / 50) * 100, 100);
                    return `
                        <div class="product-card">
                            <div class="product-icon">
                                <i class="${iconClass}"></i>
                            </div>
                            <div class="product-info">
                                <div class="product-name">${product.name}</div>
                                <div class="product-category">${product.category}</div>
                                <div class="product-stock">
                                    <div class="stock-bar">
                                        <div class="stock-fill" style="width: ${stockPercentage}%"></div>
                                    <span class="stock-text">المخزون: ${product.stock} | مباع: ${product.sold}</span>
                                </div>
                        </div>
                    `;
                }).join('') : '<div class="no-products"><i class="fas fa-box-open"></i><p>لا توجد منتجات في هذا الفرع</p></div>'}
            </div>

        <div class="sales-section">
            <h3 class="section-title"><i class="fas fa-receipt"></i> آخر عمليات الشراء</h3>
            <div class="sales-list">
                ${branch.recentSales.length > 0 ? branch.recentSales.map(sale => `
                    <div class="sale-item">
                        <div class="sale-info">
                            <div class="sale-customer">${sale.customer}</div>
                            <div class="sale-details">
                                <span><i class="fas fa-shopping-basket"></i> ${sale.items} منتجات</span>
                                <span class="sale-time">${sale.time}</span>
                            </div>
                        <div class="sale-amount">${formatCurrency(sale.total)}</div>
                `).join('') : '<div class="no-sales"><i class="fas fa-inbox"></i><p>لا توجد عمليات شراء حديثة</p></div>'}
            </div>
    `;

    document.getElementById('branchDetailModal').classList.add('active');
}

// Close detail modal
function closeDetailModal() {
    document.getElementById('branchDetailModal').classList.remove('active');
}

// Open add branch modal
function openAddModal() {
    document.getElementById('addModal').classList.add('active');
}

// Close add modal
function closeAddModal() {
    document.getElementById('addModal').classList.remove('active');
    document.getElementById('addBranchForm').reset();
}

// Open edit branch modal
function openEditModal(branchId) {
    const branch = branches.find(b => b.id === branchId);
    if (!branch) return;

    const modal = document.getElementById('editModal');
    const form = document.getElementById('editBranchForm');

    form.dataset.branchId = branchId;
    form.elements.branchName.value = branch.name;
    form.elements.branchCode.value = branch.code;
    form.elements.branchPhone.value = branch.phone;
    form.elements.branchManager.value = branch.manager;
    form.elements.branchGovernorate.value = branch.governorate;
    form.elements.branchAddress.value = branch.address;
    form.elements.branchStatus.value = branch.status;

    modal.classList.add('active');
    closeDetailModal();
}

// Close edit modal
function closeEditModal() {
    document.getElementById('editModal').classList.remove('active');
}

// Save edited branch
document.getElementById('editBranchForm').addEventListener('submit', function(e) {
    e.preventDefault();
    
    const branchId = parseInt(e.target.dataset.branchId);
    const branch = branches.find(b => b.id === branchId);
    
    if (!branch) return;

    branch.name = e.target.branchName.value;
    branch.code = e.target.branchCode.value;
    branch.phone = e.target.branchPhone.value;
    branch.manager = e.target.branchManager.value;
    branch.governorate = e.target.branchGovernorate.value;
    branch.address = e.target.branchAddress.value;
    branch.status = e.target.branchStatus.value;

    closeEditModal();
    loadBranches();
    showToast('تم تحديث بيانات الفرع بنجاح');
});

// Open delete confirmation modal
function openDeleteModal(branchId) {
    const branch = branches.find(b => b.id === branchId);
    if (!branch) return;

    document.getElementById('deleteBranchName').textContent = branch.name;
    document.getElementById('deleteModal').dataset.branchId = branchId;
    document.getElementById('deleteModal').classList.add('active');
    closeDetailModal();
}

// Close delete modal
function closeDeleteModal() {
    document.getElementById('deleteModal').classList.remove('active');
}

// Confirm delete branch
function confirmDelete() {
    const modal = document.getElementById('deleteModal');
    const branchId = parseInt(modal.dataset.branchId);
    
    branches = branches.filter(b => b.id !== branchId);
    
    closeDeleteModal();
    loadBranches();
    updateStats();
    showToast('تم حذف الفرع بنجاح');
}

// Filter branches
function filterBranches() {
    const searchTerm = document.getElementById('searchInput').value.toLowerCase();
    const statusFilter = document.getElementById('statusFilter').value;

    const filtered = branches.filter(branch => {
        const matchesSearch = branch.name.toLowerCase().includes(searchTerm) ||
                            branch.code.toLowerCase().includes(searchTerm) ||
                            branch.address.toLowerCase().includes(searchTerm) ||
                            branch.phone.includes(searchTerm);
        const matchesStatus = statusFilter === 'all' || branch.status === statusFilter;
        return matchesSearch && matchesStatus;
    });

    loadBranches(filtered);
}

// Sort branches (placeholder)
function toggleSortMenu() {
    showToast('خيار الترتيب قيد التطوير');
}

// Update statistics
function updateStats() {
    document.getElementById('totalCount').textContent = branches.length;
    document.getElementById('activeCount').textContent = branches.filter(b => b.status === 'active').length;
}

// Add new branch
document.getElementById('addBranchForm').addEventListener('submit', function(e) {
    e.preventDefault();

    const formData = new FormData(e.target);
    const newBranch = {
        id: branches.length > 0 ? Math.max(...branches.map(b => b.id)) + 1 : 1,
        name: formData.get('branchName'),
        code: formData.get('branchCode'),
        phone: formData.get('branchPhone'),
        manager: formData.get('branchManager'),
        governorate: formData.get('branchGovernorate'),
        address: formData.get('branchAddress'),
        status: formData.get('branchStatus') || 'active',
        createdAt: new Date().toISOString().split('T')[0],
        salesCount: 0,
        totalSales: 0,
        products: [],
        recentSales: []
    };

    branches.unshift(newBranch);
    updateStats();
    loadBranches();
    closeAddModal();
    showToast('تم إضافة الفرع بنجاح');
});

// Initialize modals event handlers
function initializeModals() {
    document.querySelectorAll('.modal-overlay').forEach(overlay => {
        overlay.addEventListener('click', function(e) {
            if (e.target === this) {
                this.classList.remove('active');
            }
        });
    });

    document.getElementById('toastClose').addEventListener('click', hideToast);
}

// Toast notification
function showToast(message) {
    const toast = document.getElementById('successToast');
    document.getElementById('toastMessage').textContent = message;
    toast.classList.add('show');
    setTimeout(() => hideToast(), 3000);
}

function hideToast() {
    document.getElementById('successToast').classList.remove('show');
}

// Format date helper
function formatDate(dateString) {
    const date = new Date(dateString);
    return date.toLocaleDateString('ar-EG', { year: 'numeric', month: 'short', day: 'numeric' });
}

// Format currency helper
function formatCurrency(amount) {
    return amount.toLocaleString('ar-EG') + ' ج.م';
}
