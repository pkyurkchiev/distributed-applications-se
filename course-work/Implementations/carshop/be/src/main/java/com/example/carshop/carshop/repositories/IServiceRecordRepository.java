package com.example.carshop.carshop.repositories;

import com.example.carshop.carshop.entities.impl.ServiceCarEntity;
import com.example.carshop.carshop.entities.impl.ServiceRecordEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

public interface IServiceRecordRepository extends JpaRepository<ServiceRecordEntity, Long>, JpaSpecificationExecutor<ServiceRecordEntity> {
}
