package com.example.carshop.carshop.repositories;

import com.example.carshop.carshop.entities.impl.ServiceCarEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

public interface IServiceCarRepository extends JpaRepository<ServiceCarEntity, Long>, JpaSpecificationExecutor<ServiceCarEntity> {
}
