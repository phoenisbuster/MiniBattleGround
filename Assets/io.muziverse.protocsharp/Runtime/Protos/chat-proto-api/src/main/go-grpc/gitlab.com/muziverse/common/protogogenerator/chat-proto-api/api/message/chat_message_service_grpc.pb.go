// Code generated by protoc-gen-go-grpc. DO NOT EDIT.
// versions:
// - protoc-gen-go-grpc v1.2.0
// - protoc             v3.19.1
// source: chat_message_service.proto

package message

import (
	context "context"
	domain "gitlab.com/muziverse/common/protogogenerator/chat-proto-api/domain"
	grpc "google.golang.org/grpc"
	codes "google.golang.org/grpc/codes"
	status "google.golang.org/grpc/status"
	emptypb "google.golang.org/protobuf/types/known/emptypb"
)

// This is a compile-time assertion to ensure that this generated file
// is compatible with the grpc package it is being compiled against.
// Requires gRPC-Go v1.32.0 or later.
const _ = grpc.SupportPackageIsVersion7

// ChatMessageServiceClient is the client API for ChatMessageService service.
//
// For semantics around ctx use and closing/ending streaming RPCs, please refer to https://pkg.go.dev/google.golang.org/grpc/?tab=doc#ClientConn.NewStream.
type ChatMessageServiceClient interface {
	Send(ctx context.Context, in *domain.ChatMessageRequest, opts ...grpc.CallOption) (*emptypb.Empty, error)
	Receive(ctx context.Context, in *emptypb.Empty, opts ...grpc.CallOption) (ChatMessageService_ReceiveClient, error)
}

type chatMessageServiceClient struct {
	cc grpc.ClientConnInterface
}

func NewChatMessageServiceClient(cc grpc.ClientConnInterface) ChatMessageServiceClient {
	return &chatMessageServiceClient{cc}
}

func (c *chatMessageServiceClient) Send(ctx context.Context, in *domain.ChatMessageRequest, opts ...grpc.CallOption) (*emptypb.Empty, error) {
	out := new(emptypb.Empty)
	err := c.cc.Invoke(ctx, "/chat.message.ChatMessageService/Send", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *chatMessageServiceClient) Receive(ctx context.Context, in *emptypb.Empty, opts ...grpc.CallOption) (ChatMessageService_ReceiveClient, error) {
	stream, err := c.cc.NewStream(ctx, &ChatMessageService_ServiceDesc.Streams[0], "/chat.message.ChatMessageService/Receive", opts...)
	if err != nil {
		return nil, err
	}
	x := &chatMessageServiceReceiveClient{stream}
	if err := x.ClientStream.SendMsg(in); err != nil {
		return nil, err
	}
	if err := x.ClientStream.CloseSend(); err != nil {
		return nil, err
	}
	return x, nil
}

type ChatMessageService_ReceiveClient interface {
	Recv() (*domain.StreamChatMessageResponse, error)
	grpc.ClientStream
}

type chatMessageServiceReceiveClient struct {
	grpc.ClientStream
}

func (x *chatMessageServiceReceiveClient) Recv() (*domain.StreamChatMessageResponse, error) {
	m := new(domain.StreamChatMessageResponse)
	if err := x.ClientStream.RecvMsg(m); err != nil {
		return nil, err
	}
	return m, nil
}

// ChatMessageServiceServer is the server API for ChatMessageService service.
// All implementations must embed UnimplementedChatMessageServiceServer
// for forward compatibility
type ChatMessageServiceServer interface {
	Send(context.Context, *domain.ChatMessageRequest) (*emptypb.Empty, error)
	Receive(*emptypb.Empty, ChatMessageService_ReceiveServer) error
	mustEmbedUnimplementedChatMessageServiceServer()
}

// UnimplementedChatMessageServiceServer must be embedded to have forward compatible implementations.
type UnimplementedChatMessageServiceServer struct {
}

func (UnimplementedChatMessageServiceServer) Send(context.Context, *domain.ChatMessageRequest) (*emptypb.Empty, error) {
	return nil, status.Errorf(codes.Unimplemented, "method Send not implemented")
}
func (UnimplementedChatMessageServiceServer) Receive(*emptypb.Empty, ChatMessageService_ReceiveServer) error {
	return status.Errorf(codes.Unimplemented, "method Receive not implemented")
}
func (UnimplementedChatMessageServiceServer) mustEmbedUnimplementedChatMessageServiceServer() {}

// UnsafeChatMessageServiceServer may be embedded to opt out of forward compatibility for this service.
// Use of this interface is not recommended, as added methods to ChatMessageServiceServer will
// result in compilation errors.
type UnsafeChatMessageServiceServer interface {
	mustEmbedUnimplementedChatMessageServiceServer()
}

func RegisterChatMessageServiceServer(s grpc.ServiceRegistrar, srv ChatMessageServiceServer) {
	s.RegisterService(&ChatMessageService_ServiceDesc, srv)
}

func _ChatMessageService_Send_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(domain.ChatMessageRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(ChatMessageServiceServer).Send(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/chat.message.ChatMessageService/Send",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(ChatMessageServiceServer).Send(ctx, req.(*domain.ChatMessageRequest))
	}
	return interceptor(ctx, in, info, handler)
}

func _ChatMessageService_Receive_Handler(srv interface{}, stream grpc.ServerStream) error {
	m := new(emptypb.Empty)
	if err := stream.RecvMsg(m); err != nil {
		return err
	}
	return srv.(ChatMessageServiceServer).Receive(m, &chatMessageServiceReceiveServer{stream})
}

type ChatMessageService_ReceiveServer interface {
	Send(*domain.StreamChatMessageResponse) error
	grpc.ServerStream
}

type chatMessageServiceReceiveServer struct {
	grpc.ServerStream
}

func (x *chatMessageServiceReceiveServer) Send(m *domain.StreamChatMessageResponse) error {
	return x.ServerStream.SendMsg(m)
}

// ChatMessageService_ServiceDesc is the grpc.ServiceDesc for ChatMessageService service.
// It's only intended for direct use with grpc.RegisterService,
// and not to be introspected or modified (even as a copy)
var ChatMessageService_ServiceDesc = grpc.ServiceDesc{
	ServiceName: "chat.message.ChatMessageService",
	HandlerType: (*ChatMessageServiceServer)(nil),
	Methods: []grpc.MethodDesc{
		{
			MethodName: "Send",
			Handler:    _ChatMessageService_Send_Handler,
		},
	},
	Streams: []grpc.StreamDesc{
		{
			StreamName:    "Receive",
			Handler:       _ChatMessageService_Receive_Handler,
			ServerStreams: true,
		},
	},
	Metadata: "chat_message_service.proto",
}
