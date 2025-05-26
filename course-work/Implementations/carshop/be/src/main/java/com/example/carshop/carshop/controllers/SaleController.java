package com.example.carshop.carshop.controllers;

import com.example.carshop.carshop.dto.SaleCreateDTO;
import com.example.carshop.carshop.dto.SaleDTO;
import com.example.carshop.carshop.dto.ServiceCarDTO;
import com.example.carshop.carshop.dto.request.SaleRequestDTO;
import com.example.carshop.carshop.dto.request.ServiceCarRequestDTO;
import com.example.carshop.carshop.dto.response.SaleResponseDTO;
import com.example.carshop.carshop.services.SaleService;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;


@RestController
@RequestMapping("/api/v1/sales")
@RequiredArgsConstructor
public class SaleController {

    private final SaleService saleService;

    @PostMapping("")
    @PreAuthorize("hasAuthority('EMPLOYEE')")
    public ResponseEntity<Page<SaleResponseDTO>> getAllSales(@Valid @RequestBody SaleRequestDTO saleRequestDTO) {
        return ResponseEntity.ok(saleService.getAllServiceCars(saleRequestDTO));
    }

    @GetMapping("/{id}")
    @PreAuthorize("hasAuthority('EMPLOYEE')")
    public ResponseEntity<SaleDTO> getSaleById(@PathVariable Long id) {
        return ResponseEntity.ok(saleService.getSaleById(id));
    }

    @PostMapping("/update/{id}")
    @PreAuthorize("hasAuthority('EMPLOYEE')")
    public ResponseEntity<Long> updateSale(@PathVariable Long id, @Valid @RequestBody SaleCreateDTO saleCreateDTO) {
        return ResponseEntity.ok(saleService.updateSale(saleCreateDTO));
    }

    @PostMapping("/create")
    @PreAuthorize("hasAuthority('EMPLOYEE')")
    public ResponseEntity<Long> createSale(@Valid @RequestBody SaleCreateDTO saleCreateDto) {
        return ResponseEntity.ok().body(saleService.createSale(saleCreateDto));
    }

    @PostMapping("/delete/{id}")
    @PreAuthorize("hasAuthority('EMPLOYEE')")
    public ResponseEntity<Void> deleteSale(@PathVariable Long id) {
        saleService.deleteSale(id);
        return ResponseEntity.ok().build();
    }

}
