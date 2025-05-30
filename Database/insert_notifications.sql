-- Giả định các giá trị LABEL đã được định nghĩa như sau:
-- CAPBAC: TRUONG = 10, NHANVIEN = 20, SINHVIEN = 30
-- DONVI: HOA = 103, HANHCHINH = 104
-- COSO: COSO1 = 201, COSO2 = 202

-- Hàm gán label (tùy hệ thống bạn dùng, ví dụ 3-level như sau):
-- SA_LABEL_ADMIN.CREATE_LABEL(policy_name, label_tag, label_value, label_text, data_label, label_comp)

-- t1: Cho toàn bộ trưởng đơn vị (chung chung – mọi đơn vị/cơ sở)
INSERT INTO THONGBAO (NOIDUNG, LABEL) VALUES ('Thông báo cho tất cả trưởng đơn vị', 10);

-- t2: Cho tất cả nhân viên
INSERT INTO THONGBAO (NOIDUNG, LABEL) VALUES ('Thông báo cho tất cả nhân viên', 20);

-- t3: Cho tất cả sinh viên
INSERT INTO THONGBAO (NOIDUNG, LABEL) VALUES ('Thông báo cho tất cả sinh viên', 30);

-- t4: Sinh viên HÓA - CS1
INSERT INTO THONGBAO (NOIDUNG, LABEL) VALUES ('TB SV Hóa - CS1', 
    SA_LABEL_ADMIN.LABEL_TO_NUM('THONGBAO_POLICY', 'SINHVIEN:HOA:COSO1'));

-- t5: Sinh viên HÓA - CS2
INSERT INTO THONGBAO (NOIDUNG, LABEL) VALUES ('TB SV Hóa - CS2', 
    SA_LABEL_ADMIN.LABEL_TO_NUM('THONGBAO_POLICY', 'SINHVIEN:HOA:COSO2'));

-- t6: SV HÓA cả hai cơ sở => tạo hai bản hoặc dùng MASK nếu cần
-- (Dùng nhãn gộp nếu OLS cho phép, hoặc cần INSERT hai dòng với CS1 và CS2)

-- t8: Trưởng HÓA CS1
INSERT INTO THONGBAO (NOIDUNG, LABEL) VALUES ('TB Trưởng Hóa CS1',
    SA_LABEL_ADMIN.LABEL_TO_NUM('THONGBAO_POLICY', 'TRUONG:HOA:COSO1'));

-- t9: Trưởng HÓA CS1 và CS2
-- Ghi 2 bản ghi hoặc dùng label union nếu được phép

COMMIT;
