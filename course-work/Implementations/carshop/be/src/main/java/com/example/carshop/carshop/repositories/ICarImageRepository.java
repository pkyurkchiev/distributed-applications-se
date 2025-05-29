package com.example.carshop.carshop.repositories;

import com.example.carshop.carshop.entities.impl.CarImageEntity;
import org.springframework.data.jpa.repository.JpaRepository;

public interface ICarImageRepository extends JpaRepository<CarImageEntity, Long> {
}
