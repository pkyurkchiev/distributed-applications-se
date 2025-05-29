package com.example.carshop.carshop.services;

import com.example.carshop.carshop.dto.SaleCreateDTO;
import com.example.carshop.carshop.dto.SaleDTO;
import com.example.carshop.carshop.dto.ServiceCarDTO;
import com.example.carshop.carshop.dto.ServiceRecordDTO;
import com.example.carshop.carshop.dto.request.SaleRequestDTO;
import com.example.carshop.carshop.dto.request.ServiceRecordRequestDTO;
import com.example.carshop.carshop.dto.response.SaleResponseDTO;
import com.example.carshop.carshop.dto.response.ServiceRecordResponseDTO;
import com.example.carshop.carshop.dto.specification.SaleSpecification;
import com.example.carshop.carshop.dto.specification.ServiceRecordSpecification;
import com.example.carshop.carshop.entities.impl.*;
import com.example.carshop.carshop.mappers.SaleMapper;
import com.example.carshop.carshop.mappers.ServiceRecordMapper;
import com.example.carshop.carshop.repositories.*;
import com.example.carshop.shared.exceptions.carshop.EntityNotFoundException;
import jakarta.transaction.Transactional;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;

import java.util.Map;

@Service
@RequiredArgsConstructor
public class ServiceRecordService {

    private final IServiceRecordRepository serviceRecordRepository;

    private final IEmployeeRepository employeeRepository;

    private final IServiceCarRepository serviceCarRepository;

    @Transactional
    public Page<ServiceRecordResponseDTO> getAllServiceRecords(ServiceRecordRequestDTO serviceRecordRequestDTO) {

        Sort sort = serviceRecordRequestDTO.getAsc() ? Sort.by(serviceRecordRequestDTO.getSortBy()).ascending() : Sort.by(serviceRecordRequestDTO.getSortBy()).descending();
        Pageable pageable = PageRequest.of(serviceRecordRequestDTO.getPage(), serviceRecordRequestDTO.getSize(), sort);
        Page<ServiceRecordEntity> serviceCarPage = serviceRecordRepository.findAll(new ServiceRecordSpecification(serviceRecordRequestDTO.getFilters()), pageable);
        return serviceCarPage.map(ServiceRecordMapper::toResponseDTO);
    }

    @Transactional
    public Page<ServiceRecordResponseDTO> getAllServiceRecordsCustomer(Long customerId, ServiceRecordRequestDTO serviceRecordRequestDTO) {
        Map<String, String> filters = serviceRecordRequestDTO.getFilters();
        filters.put("customer_id", customerId.toString());
        serviceRecordRequestDTO.setFilters(filters);
        Sort sort = serviceRecordRequestDTO.getAsc() ? Sort.by(serviceRecordRequestDTO.getSortBy()).ascending() : Sort.by(serviceRecordRequestDTO.getSortBy()).descending();
        Pageable pageable = PageRequest.of(serviceRecordRequestDTO.getPage(), serviceRecordRequestDTO.getSize(), sort);
        Page<ServiceRecordEntity> serviceCarPage = serviceRecordRepository.findAll(new ServiceRecordSpecification(serviceRecordRequestDTO.getFilters()), pageable);
        return serviceCarPage.map(ServiceRecordMapper::toResponseDTO);
    }

    public ServiceRecordDTO getServiceRecordById(Long id) {
        return serviceRecordRepository.findById(id)
                .map(ServiceRecordMapper::toDTO)
                .orElseThrow(() -> new EntityNotFoundException("Sale not found with id: " + id, 404));
    }
//
    @Transactional
    public Long createServiceRecord(ServiceRecordDTO serviceRecordDTO) {

        ServiceRecordEntity serviceRecordEntity = ServiceRecordMapper.toEntity(serviceRecordDTO);

        EmployeeEntity employee = employeeRepository.findById(serviceRecordDTO.getEmployeeId())
                .orElseThrow(() -> new EntityNotFoundException("Customer not found with ID: " + serviceRecordDTO.getEmployeeId(), 404));

        ServiceCarEntity serviceCar = serviceCarRepository.findById(serviceRecordDTO.getServiceCarId())
                .orElseThrow(() -> new EntityNotFoundException("Car not found with ID: " + serviceRecordDTO.getServiceCarId(), 404));


        ServiceRecordEntity serviceRecordUpdated = serviceRecordRepository.save(serviceRecordEntity);

        serviceRecordUpdated.setEmployeeId(employee);
        serviceRecordUpdated.setServiceCarId(serviceCar);

        return serviceRecordRepository.save(serviceRecordUpdated).getId();
    }
//
    @Transactional
    public Long updateServiceRecord(ServiceRecordDTO serviceRecordDTO) {
        ServiceRecordEntity serviceRecordEntity = serviceRecordRepository.findById(serviceRecordDTO.getId())
                .orElseThrow(() -> new EntityNotFoundException("Sale not found with id: " + serviceRecordDTO.getId(), 404));

        EmployeeEntity employee = employeeRepository.findById(serviceRecordDTO.getEmployeeId())
                .orElseThrow(() -> new EntityNotFoundException("Customer not found with ID: " + serviceRecordDTO.getEmployeeId(), 404));

        ServiceCarEntity serviceCar = serviceCarRepository.findById(serviceRecordDTO.getServiceCarId())
                .orElseThrow(() -> new EntityNotFoundException("Car not found with ID: " + serviceRecordDTO.getServiceCarId(), 404));


        ServiceRecordMapper.toEntity(serviceRecordEntity, serviceRecordDTO);

        serviceRecordEntity.setEmployeeId(employee);
        serviceRecordEntity.setServiceCarId(serviceCar);

        return serviceRecordRepository.save(serviceRecordEntity).getId();
    }

    @Transactional
    public void deleteServiceRecord(Long id) {
        serviceRecordRepository.deleteById(id);
    }

}
