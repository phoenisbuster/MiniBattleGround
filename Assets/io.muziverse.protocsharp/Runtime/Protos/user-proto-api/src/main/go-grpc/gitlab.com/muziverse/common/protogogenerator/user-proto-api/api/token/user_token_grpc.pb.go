// Code generated by protoc-gen-go-grpc. DO NOT EDIT.
// versions:
// - protoc-gen-go-grpc v1.2.0
// - protoc             v3.19.1
// source: user_token.proto

package token

import (
	context "context"
	domain "gitlab.com/muziverse/common/protogogenerator/user-proto-api/domain"
	grpc "google.golang.org/grpc"
	codes "google.golang.org/grpc/codes"
	status "google.golang.org/grpc/status"
	emptypb "google.golang.org/protobuf/types/known/emptypb"
)

// This is a compile-time assertion to ensure that this generated file
// is compatible with the grpc package it is being compiled against.
// Requires gRPC-Go v1.32.0 or later.
const _ = grpc.SupportPackageIsVersion7

// UserTokenServiceClient is the client API for UserTokenService service.
//
// For semantics around ctx use and closing/ending streaming RPCs, please refer to https://pkg.go.dev/google.golang.org/grpc/?tab=doc#ClientConn.NewStream.
type UserTokenServiceClient interface {
	RefreshToken(ctx context.Context, in *emptypb.Empty, opts ...grpc.CallOption) (*domain.AccessFlowResponse, error)
}

type userTokenServiceClient struct {
	cc grpc.ClientConnInterface
}

func NewUserTokenServiceClient(cc grpc.ClientConnInterface) UserTokenServiceClient {
	return &userTokenServiceClient{cc}
}

func (c *userTokenServiceClient) RefreshToken(ctx context.Context, in *emptypb.Empty, opts ...grpc.CallOption) (*domain.AccessFlowResponse, error) {
	out := new(domain.AccessFlowResponse)
	err := c.cc.Invoke(ctx, "/identity.token.UserTokenService/RefreshToken", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

// UserTokenServiceServer is the server API for UserTokenService service.
// All implementations must embed UnimplementedUserTokenServiceServer
// for forward compatibility
type UserTokenServiceServer interface {
	RefreshToken(context.Context, *emptypb.Empty) (*domain.AccessFlowResponse, error)
	mustEmbedUnimplementedUserTokenServiceServer()
}

// UnimplementedUserTokenServiceServer must be embedded to have forward compatible implementations.
type UnimplementedUserTokenServiceServer struct {
}

func (UnimplementedUserTokenServiceServer) RefreshToken(context.Context, *emptypb.Empty) (*domain.AccessFlowResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method RefreshToken not implemented")
}
func (UnimplementedUserTokenServiceServer) mustEmbedUnimplementedUserTokenServiceServer() {}

// UnsafeUserTokenServiceServer may be embedded to opt out of forward compatibility for this service.
// Use of this interface is not recommended, as added methods to UserTokenServiceServer will
// result in compilation errors.
type UnsafeUserTokenServiceServer interface {
	mustEmbedUnimplementedUserTokenServiceServer()
}

func RegisterUserTokenServiceServer(s grpc.ServiceRegistrar, srv UserTokenServiceServer) {
	s.RegisterService(&UserTokenService_ServiceDesc, srv)
}

func _UserTokenService_RefreshToken_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(emptypb.Empty)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(UserTokenServiceServer).RefreshToken(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/identity.token.UserTokenService/RefreshToken",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(UserTokenServiceServer).RefreshToken(ctx, req.(*emptypb.Empty))
	}
	return interceptor(ctx, in, info, handler)
}

// UserTokenService_ServiceDesc is the grpc.ServiceDesc for UserTokenService service.
// It's only intended for direct use with grpc.RegisterService,
// and not to be introspected or modified (even as a copy)
var UserTokenService_ServiceDesc = grpc.ServiceDesc{
	ServiceName: "identity.token.UserTokenService",
	HandlerType: (*UserTokenServiceServer)(nil),
	Methods: []grpc.MethodDesc{
		{
			MethodName: "RefreshToken",
			Handler:    _UserTokenService_RefreshToken_Handler,
		},
	},
	Streams:  []grpc.StreamDesc{},
	Metadata: "user_token.proto",
}
