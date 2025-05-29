package com.example.carshop.carshop.repositories;

import com.example.carshop.carshop.entities.impl.CarEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;
import org.springframework.data.jpa.repository.Query;

import java.util.List;


public interface ICarRepository extends JpaRepository<CarEntity, Long>, JpaSpecificationExecutor<CarEntity> {

    @Query("SELECT c FROM cars c WHERE c.isInStock = true")
    List<CarEntity> findByInStockTrue();

}
