package com.example.carshop.carshop.controllers;


import com.example.carshop.carshop.dto.SaleDTO;
import com.example.carshop.carshop.dto.ServiceRecordDTO;
import com.example.carshop.carshop.dto.request.ServiceRecordRequestDTO;
import com.example.carshop.carshop.dto.response.ServiceRecordResponseDTO;
import com.example.carshop.carshop.services.ServiceRecordService;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/v1/service-records")
@RequiredArgsConstructor
public class ServiceRecordController {

    private final ServiceRecordService serviceRecordService;

    @PostMapping()
    @PreAuthorize("hasAuthority('EMPLOYEE')")
    public ResponseEntity<Page<ServiceRecordResponseDTO>> getAllServiceRecords(@Valid @RequestBody ServiceRecordRequestDTO serviceRecordRequestDTO) {
        return ResponseEntity.ok(serviceRecordService.getAllServiceRecords(serviceRecordRequestDTO));
    }

    @PostMapping("/my/{id}")
    @PreAuthorize("hasAuthority('CUSTOMER')")
    public ResponseEntity<Page<ServiceRecordResponseDTO>> getAllServiceRecordsCustomer(@PathVariable Long id, @Valid @RequestBody ServiceRecordRequestDTO serviceRecordRequestDTO) {
        return ResponseEntity.ok(serviceRecordService.getAllServiceRecordsCustomer(id, serviceRecordRequestDTO));
    }

    @GetMapping("/{id}")
    @PreAuthorize("hasAnyAuthority('EMPLOYEE', 'CUSTOMER')")
    public ResponseEntity< ServiceRecordDTO> getSServiceRecordById(@PathVariable Long id) {
        return ResponseEntity.ok(serviceRecordService.getServiceRecordById(id));
    }

//
    @PostMapping("/update/{id}")
    @PreAuthorize("hasAuthority('EMPLOYEE')")
    public ResponseEntity<Long> updateSale(@PathVariable Long id, @Valid @RequestBody ServiceRecordDTO serviceRecordDTO) {
        return ResponseEntity.ok(serviceRecordService.updateServiceRecord(serviceRecordDTO));
    }
//
    @PostMapping("/create")
    @PreAuthorize("hasAuthority('EMPLOYEE')")
    public ResponseEntity<Long> createServiceRecord(@Valid @RequestBody ServiceRecordDTO serviceRecordDTO) {
        return ResponseEntity.ok().body(serviceRecordService.createServiceRecord(serviceRecordDTO));
    }

    @PostMapping("/delete/{id}")
    @PreAuthorize("hasAuthority('EMPLOYEE')")
    public ResponseEntity<Void> deleteServiceRecord(@PathVariable Long id) {
        serviceRecordService.deleteServiceRecord(id);
        return ResponseEntity.ok().build();
    }

}
