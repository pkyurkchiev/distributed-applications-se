package com.example.carshop.carshop.controllers;

import com.example.carshop.carshop.dto.CarListDTO;
import com.example.carshop.carshop.dto.ServiceCarDTO;
import com.example.carshop.carshop.dto.request.ServiceCarRequestDTO;
import com.example.carshop.carshop.services.ServiceCarService;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/v1/service-cars/my")
@RequiredArgsConstructor
public class ServiceCarController {

    private final ServiceCarService serviceCarService;

    @PostMapping("/{id}")
    @PreAuthorize("hasAnyAuthority('EMPLOYEE', 'CUSTOMER')")
    public ResponseEntity<Page<ServiceCarDTO>> getAllCars(@PathVariable Long id, @Valid @RequestBody ServiceCarRequestDTO serviceCarRequestDTO) {
        return ResponseEntity.ok(serviceCarService.getAllServiceCars(id, serviceCarRequestDTO));
    }

    @GetMapping
    @PreAuthorize("hasAnyAuthority('EMPLOYEE', 'CUSTOMER')")
    public ResponseEntity<List<ServiceCarDTO>> getAllCarsNoBody() {
        return ResponseEntity.ok(serviceCarService.getAllServiceRecordsNoBody());
    }

    @GetMapping("/{id}")
    @PreAuthorize("hasAnyAuthority('EMPLOYEE', 'CUSTOMER')")
    public ResponseEntity<ServiceCarDTO> getCarById(@PathVariable Long id) {
        return ResponseEntity.ok(serviceCarService.getServiceCarById(id));
    }

    @PostMapping("/update/{id}")
    @PreAuthorize("hasAnyAuthority('CUSTOMER')")
    public ResponseEntity<Long> updateServiceCar(@PathVariable Long id, @Valid @RequestBody ServiceCarDTO serviceCarDTO) {
        return ResponseEntity.ok(serviceCarService.updateServiceCar(serviceCarDTO));
    }

    @PostMapping("/create/{id}")
    @PreAuthorize("hasAnyAuthority('CUSTOMER')")
    public ResponseEntity<Long> createServiceCar(@PathVariable Long id, @Valid @RequestBody ServiceCarDTO serviceCarDTO) {
         return ResponseEntity.ok().body(serviceCarService.createServiceCar(id, serviceCarDTO));
    }

    @PostMapping("/delete/{id}")
    @PreAuthorize("hasAnyAuthority('CUSTOMER')")
    public ResponseEntity<Void> deleteServiceCar(@PathVariable Long id) {
        serviceCarService.deleteServiceCarById(id);
        return ResponseEntity.ok().build();
    }
}