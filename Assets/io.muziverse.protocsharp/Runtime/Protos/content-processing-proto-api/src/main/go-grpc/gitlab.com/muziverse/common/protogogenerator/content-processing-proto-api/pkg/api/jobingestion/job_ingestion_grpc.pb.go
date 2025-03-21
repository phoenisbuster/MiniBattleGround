// Code generated by protoc-gen-go-grpc. DO NOT EDIT.
// versions:
// - protoc-gen-go-grpc v1.2.0
// - protoc             v3.19.1
// source: job_ingestion.proto

package jobingestion

import (
	context "context"
	grpc "google.golang.org/grpc"
	codes "google.golang.org/grpc/codes"
	status "google.golang.org/grpc/status"
)

// This is a compile-time assertion to ensure that this generated file
// is compatible with the grpc package it is being compiled against.
// Requires gRPC-Go v1.32.0 or later.
const _ = grpc.SupportPackageIsVersion7

// JobIngestionClient is the client API for JobIngestion service.
//
// For semantics around ctx use and closing/ending streaming RPCs, please refer to https://pkg.go.dev/google.golang.org/grpc/?tab=doc#ClientConn.NewStream.
type JobIngestionClient interface {
	CreateJobIngestion(ctx context.Context, in *CreateJobIngestionRequest, opts ...grpc.CallOption) (*CreateJobIngestionResponse, error)
	GetJobsIngestion(ctx context.Context, in *GetJobsIngestionRequest, opts ...grpc.CallOption) (*GetJobsIngestionResponse, error)
	GetJobIngestionDetailById(ctx context.Context, in *GetJobIngestionDetailByIdRequest, opts ...grpc.CallOption) (*GetJobIngestionDetailByIdResponse, error)
}

type jobIngestionClient struct {
	cc grpc.ClientConnInterface
}

func NewJobIngestionClient(cc grpc.ClientConnInterface) JobIngestionClient {
	return &jobIngestionClient{cc}
}

func (c *jobIngestionClient) CreateJobIngestion(ctx context.Context, in *CreateJobIngestionRequest, opts ...grpc.CallOption) (*CreateJobIngestionResponse, error) {
	out := new(CreateJobIngestionResponse)
	err := c.cc.Invoke(ctx, "/contentprocessing.jobingestion.JobIngestion/CreateJobIngestion", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *jobIngestionClient) GetJobsIngestion(ctx context.Context, in *GetJobsIngestionRequest, opts ...grpc.CallOption) (*GetJobsIngestionResponse, error) {
	out := new(GetJobsIngestionResponse)
	err := c.cc.Invoke(ctx, "/contentprocessing.jobingestion.JobIngestion/GetJobsIngestion", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *jobIngestionClient) GetJobIngestionDetailById(ctx context.Context, in *GetJobIngestionDetailByIdRequest, opts ...grpc.CallOption) (*GetJobIngestionDetailByIdResponse, error) {
	out := new(GetJobIngestionDetailByIdResponse)
	err := c.cc.Invoke(ctx, "/contentprocessing.jobingestion.JobIngestion/GetJobIngestionDetailById", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

// JobIngestionServer is the server API for JobIngestion service.
// All implementations must embed UnimplementedJobIngestionServer
// for forward compatibility
type JobIngestionServer interface {
	CreateJobIngestion(context.Context, *CreateJobIngestionRequest) (*CreateJobIngestionResponse, error)
	GetJobsIngestion(context.Context, *GetJobsIngestionRequest) (*GetJobsIngestionResponse, error)
	GetJobIngestionDetailById(context.Context, *GetJobIngestionDetailByIdRequest) (*GetJobIngestionDetailByIdResponse, error)
	mustEmbedUnimplementedJobIngestionServer()
}

// UnimplementedJobIngestionServer must be embedded to have forward compatible implementations.
type UnimplementedJobIngestionServer struct {
}

func (UnimplementedJobIngestionServer) CreateJobIngestion(context.Context, *CreateJobIngestionRequest) (*CreateJobIngestionResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method CreateJobIngestion not implemented")
}
func (UnimplementedJobIngestionServer) GetJobsIngestion(context.Context, *GetJobsIngestionRequest) (*GetJobsIngestionResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method GetJobsIngestion not implemented")
}
func (UnimplementedJobIngestionServer) GetJobIngestionDetailById(context.Context, *GetJobIngestionDetailByIdRequest) (*GetJobIngestionDetailByIdResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method GetJobIngestionDetailById not implemented")
}
func (UnimplementedJobIngestionServer) mustEmbedUnimplementedJobIngestionServer() {}

// UnsafeJobIngestionServer may be embedded to opt out of forward compatibility for this service.
// Use of this interface is not recommended, as added methods to JobIngestionServer will
// result in compilation errors.
type UnsafeJobIngestionServer interface {
	mustEmbedUnimplementedJobIngestionServer()
}

func RegisterJobIngestionServer(s grpc.ServiceRegistrar, srv JobIngestionServer) {
	s.RegisterService(&JobIngestion_ServiceDesc, srv)
}

func _JobIngestion_CreateJobIngestion_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(CreateJobIngestionRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(JobIngestionServer).CreateJobIngestion(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/contentprocessing.jobingestion.JobIngestion/CreateJobIngestion",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(JobIngestionServer).CreateJobIngestion(ctx, req.(*CreateJobIngestionRequest))
	}
	return interceptor(ctx, in, info, handler)
}

func _JobIngestion_GetJobsIngestion_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(GetJobsIngestionRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(JobIngestionServer).GetJobsIngestion(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/contentprocessing.jobingestion.JobIngestion/GetJobsIngestion",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(JobIngestionServer).GetJobsIngestion(ctx, req.(*GetJobsIngestionRequest))
	}
	return interceptor(ctx, in, info, handler)
}

func _JobIngestion_GetJobIngestionDetailById_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(GetJobIngestionDetailByIdRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(JobIngestionServer).GetJobIngestionDetailById(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/contentprocessing.jobingestion.JobIngestion/GetJobIngestionDetailById",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(JobIngestionServer).GetJobIngestionDetailById(ctx, req.(*GetJobIngestionDetailByIdRequest))
	}
	return interceptor(ctx, in, info, handler)
}

// JobIngestion_ServiceDesc is the grpc.ServiceDesc for JobIngestion service.
// It's only intended for direct use with grpc.RegisterService,
// and not to be introspected or modified (even as a copy)
var JobIngestion_ServiceDesc = grpc.ServiceDesc{
	ServiceName: "contentprocessing.jobingestion.JobIngestion",
	HandlerType: (*JobIngestionServer)(nil),
	Methods: []grpc.MethodDesc{
		{
			MethodName: "CreateJobIngestion",
			Handler:    _JobIngestion_CreateJobIngestion_Handler,
		},
		{
			MethodName: "GetJobsIngestion",
			Handler:    _JobIngestion_GetJobsIngestion_Handler,
		},
		{
			MethodName: "GetJobIngestionDetailById",
			Handler:    _JobIngestion_GetJobIngestionDetailById_Handler,
		},
	},
	Streams:  []grpc.StreamDesc{},
	Metadata: "job_ingestion.proto",
}
