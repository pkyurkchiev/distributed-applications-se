package com.example.carshop.carshop.controllers;

import com.example.carshop.authentication.dto.AuthenticationResponseDTO;
import com.example.carshop.carshop.dto.CarDTO;
import com.example.carshop.carshop.dto.CarListDTO;
import com.example.carshop.carshop.dto.request.CarRequestListDTO;
import com.example.carshop.carshop.services.CarService;
import com.example.carshop.shared.exceptions.carshop.EntityNotFoundException;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/v1/cars")
@RequiredArgsConstructor
public class CarController {

    private final CarService carService;

    @PostMapping
    public ResponseEntity<Page<CarListDTO>> getAllCars(@Valid @RequestBody CarRequestListDTO carRequestListDTO) {
        return ResponseEntity.ok(carService.getAllCars(carRequestListDTO));
    }

    @GetMapping
    public ResponseEntity<List<CarListDTO>> getAllCarsNoBody() {
        return ResponseEntity.ok(carService.getAllCarsNoBody());
    }

    @GetMapping("/in-stock")
    public ResponseEntity<List<CarListDTO>> getAllInStockCarsNoBody() {
        return ResponseEntity.ok(carService.getAllInStockCarsNoBody());
    }

    @GetMapping("/{id}")
    public ResponseEntity<CarDTO> getCarById(@PathVariable Long id) {
        return ResponseEntity.ok(carService.getCarById(id));
    }

    @PostMapping("/update/{id}")
    @PreAuthorize("hasAuthority('EMPLOYEE')")
    public ResponseEntity<Long> updateCar(@PathVariable Long id, @Valid @RequestBody CarDTO carDTO) {
        return ResponseEntity.ok(carService.updateCar(carDTO));
    }

    @PostMapping("/create")
    @PreAuthorize("hasAuthority('EMPLOYEE')")
    public ResponseEntity<Long> createCar(@Valid @RequestBody CarDTO carDTO) {
        return ResponseEntity.ok().body(carService.createCar(carDTO));
    }

    @PostMapping("/delete/{id}")
    @PreAuthorize("hasAuthority('EMPLOYEE')")
    public ResponseEntity<Void> deleteCar(@PathVariable Long id) {
        carService.deleteCarById(id);
        return ResponseEntity.ok().build();
    }



}